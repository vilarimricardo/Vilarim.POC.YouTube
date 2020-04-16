using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Vilarim.POC.YouTube.Domain.Actions
{
    public class Action<T> : IRequest<T>
    {
    }

    public class ActionStatus
    {
        public string Status { get; }
        public static ActionStatus OK = new ActionStatus("OK");
        public static ActionStatus ERROR = new ActionStatus("ERROR");

        public ActionStatus(string status)
        {
            Status = status;
        }
    }

    public abstract class BaseActionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        //private readonly IServiceProvider _serviceProvider;
        protected readonly IMediator _mediatr;

        protected BaseActionHandler(IServiceProvider serviceProvider){
            //_serviceProvider = serviceProvider;
            _mediatr = serviceProvider.GetRequiredService<IMediator>();
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}