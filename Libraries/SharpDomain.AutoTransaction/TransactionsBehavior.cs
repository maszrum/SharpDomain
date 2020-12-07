using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.AutoTransaction
{
    internal class TransactionsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ServiceFactory _serviceFactory;
        
        public TransactionsBehavior(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var handlerType = typeof(TransactionHandler<,>)
                .MakeGenericType(typeof(TRequest), typeof(TResponse));
            
            var enumerableHandlerType = typeof(IEnumerable<>)
                .MakeGenericType(handlerType);
            
            var transactionHandlers = _serviceFactory.Invoke(enumerableHandlerType);
            
            if (transactionHandlers is IEnumerable<TransactionHandler<TRequest, TResponse>> handlersTyped)
            {
                return handlersTyped
                    .Aggregate(
                        seed: next, 
                        func: (n, pipeline) => () => pipeline!.Handle(request, cancellationToken, n))();
            }
            
            return next();
        }
    }
}