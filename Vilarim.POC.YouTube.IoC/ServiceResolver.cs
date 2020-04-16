using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vilarim.POC.YouTube.Domain.Actions;
using Vilarim.POC.YouTube.Infra;
using Vilarim.POC.YouTube.Infra.ActionsHandler;
using Vilarim.POC.YouTube.Infra.Cloud;
using Vilarim.POC.YouTube.Infra.Contracts.Cloud;
using Vilarim.POC.YouTube.Infra.Contracts.Repo;
using Vilarim.POC.YouTube.Infra.Repo;

namespace Vilarim.POC.YouTube.IoC
{
    public class ServiceResolver
    {
        public static void ResolveCommons(IServiceCollection services)
        {
            ResolveSingleton(services);
            ResolveScoped(services);
            ResolveMediatR(services);
        }

        private static void ResolveMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(SearchOnYouTubeApi).Assembly);
            services.AddMediatR(typeof(PersistSearchHandler).Assembly);
        }


        private static void ResolveScoped(IServiceCollection services)
        {
            services.AddScoped<IRepository, PostgresRepository>();
            services.AddScoped<IYouTubeRepository, YouTubeRepository>();
            //TODO: Remover após implementação do postgres
           // services.AddDbContext<YouTubeContext>(o => o.UseInMemoryDatabase("TestScope").EnableSensitiveDataLogging(true), contextLifetime: ServiceLifetime.Scoped);
        }

        private static void ResolveSingleton(IServiceCollection services)
        {

        }
    }
}
