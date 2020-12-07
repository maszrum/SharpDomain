using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.Transactions
{
    internal class TransactionsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ServiceFactory _serviceFactory;

        public TransactionsBehavior(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var handlerType = typeof(TransactionHandler<,>)
                .MakeGenericType(typeof(TRequest), typeof(TResponse));
            
            // TODO: dynamically find handlers
            var transactionHandlers = new[] { _serviceFactory.Invoke(handlerType) as TransactionHandler<TRequest, TResponse> };
            
            var response = await transactionHandlers
                .Aggregate(
                    seed: next, 
                    func: (n, pipeline) => () => pipeline.Handle(request, cancellationToken, n))();
                
            return response;
        }
    }
}