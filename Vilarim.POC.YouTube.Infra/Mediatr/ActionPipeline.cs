using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vilarim.POC.YouTube.Infra.MediatR
{
    public class ActionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : Domain.Actions.Action<TResponse>
            where TResponse : class
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var result = await next.Invoke();

                return result;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                throw ex;
             }
        }
    }
}
