using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vilarim.POC.YouTube.Api;
using Vilarim.POC.YouTube.Infra;
using Vilarim.POC.YouTube.Infra.Contracts.Repo;
using Vilarim.POC.YouTube.Infra.Repo;
using Vilarim.POC.YouTube.IoC;

namespace Vilarim.POC.YouTube.Test
{
    public class TestBase
    {
        private TestServer testSerser;
        protected bool UseMock;

        protected TestServer TestServer
        {
            get
            {

                if (testSerser == null)
                {
                    var webHost = new WebHostBuilder()
                     .UseStartup<Startup>() // <- Não usar o setup padrão pois ele injeta os servicos reais, não mocados 
                     .ConfigureServices(x => ServicesResolverTest.Resolve(x));

                    testSerser = new TestServer(webHost);
                }

                return testSerser;
            }
        }

        protected IServiceProvider ServiceProvider
        {
            get
            {
                return TestServer.Host.Services;
            }
        }

        protected HttpClient CreateTestServerClient()
        {
            var cliente = TestServer.CreateClient();

            return cliente;
        }

        protected IServiceScope CreateScope()
        {
            return ServiceProvider.CreateScope();
        }
    }

    public class ServicesResolverTest
    {
        public static void Resolve(IServiceCollection services)
        {
            services.AddDbContext<YouTubeContext>(opt => opt.UseInMemoryDatabase(databaseName: "TesteScope"),
            ServiceLifetime.Scoped,
            ServiceLifetime.Scoped);
        }
    }

    public static class JsonHelper
    {
        public static string JsonSerialize<T>(this T item) where T : class =>
            JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

        public static T JsonDeserialize<T>(this string json) where T : class => JsonConvert.DeserializeObject<T>(json);

    }

    public static class TaskHelper
    {
        private static CancellationTokenSource _source;
        private static ConcurrentDictionary<int, bool> _dcTasksFinalizadas;

        public static void DefinirTokenSourcePrincipal(CancellationTokenSource source)
        {
            _source = source;
            _dcTasksFinalizadas = new ConcurrentDictionary<int, bool>();
        }

        public static void CancelarTokenSourcePrincipal()
        {
            _source.Cancel();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Não tratar")]
        public static async Task RunLongTaskAsync(Task metodo, CancellationToken cancellationToken)
        {
            var task = new Task(async () =>
            {
                var taskId = Task.CurrentId.Value;
                bool reiniciarTask = false;

                try
                {
                    _dcTasksFinalizadas.TryAdd(taskId, false);
                    await metodo;
                }
                catch (TaskCanceledException) { /*Task foi cancelada não faz nada não é erro */ }
                catch (OperationCanceledException) { /*Task foi cancelada não faz nada não é erro */ }
                catch (Exception)
                {
                    reiniciarTask = true;
                }
                finally
                {
                    _dcTasksFinalizadas.TryRemove(taskId, out bool r);
                }

                if (reiniciarTask)
                    await RunLongTaskAsync(metodo, cancellationToken);
            }, cancellationToken, TaskCreationOptions.LongRunning);
            task.Start();

            await Task.CompletedTask;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Não tratar")]
        public static async Task RunLongTaskAsync(Task metodo)
        {
            var task = new Task(async () =>
            {
                var taskId = Task.CurrentId.Value;
                bool reiniciarTask = false;

                try
                {
                    _dcTasksFinalizadas.TryAdd(taskId, false);
                    await metodo;
                }
                catch (TaskCanceledException) { /*Task foi cancelada não faz nada não é erro */ }
                catch (OperationCanceledException) { /*Task foi cancelada não faz nada não é erro */ }
                catch (Exception)
                {
                    reiniciarTask = true;
                }
                finally
                {
                    _dcTasksFinalizadas.TryRemove(taskId, out bool r);
                }

                if (reiniciarTask)
                    await RunLongTaskAsync(metodo);
            }, TaskCreationOptions.LongRunning);
            task.Start();

            await Task.CompletedTask;
        }

        public static bool PossuiTaskNaoFinalizada()
        {
            return _dcTasksFinalizadas.Any(x => !x.Value);
        }

        public static void LimparListaTasks()
        {
            _dcTasksFinalizadas.Clear();
            _dcTasksFinalizadas = null;
            _source.Dispose();
            _source = null;
        }

        private static readonly TaskFactory _myTaskFactory = new
          TaskFactory(CancellationToken.None,
                      TaskCreationOptions.None,
                      TaskContinuationOptions.None,
                      TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory
              .StartNew<Task<TResult>>(func)
              .Unwrap<TResult>()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory
              .StartNew<Task>(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }
    }
}