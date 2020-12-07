using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.Transactions
{
    public abstract class TransactionHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
        
        protected bool IsRollingBackException(Exception exception)
        {
            // TODO: check if exception is rolling back
            return true;
        }
    }
}