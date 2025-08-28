using MediatR;
using OfflineTicketing.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Behaviours
{
    public class ResultBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (Exception ex)
            {
              
                if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var genericType = typeof(TResponse).GetGenericArguments()[0];
                    var resultType = typeof(Result<>).MakeGenericType(genericType);
                    var failMethod = resultType.GetMethod(nameof(Result<object>.Fail), new[] { typeof(string) });
                    return (TResponse)failMethod.Invoke(null, new object[] { ex.Message });
                }

            
                throw;
            }
        }
    }
}