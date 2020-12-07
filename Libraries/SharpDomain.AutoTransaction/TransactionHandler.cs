using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.AutoTransaction
{
    public abstract class TransactionHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
        
        protected bool IsRollingBackException(Exception exception)
        {
            var attributeType = typeof(NotRollingBackAttribute);
            var attribute = exception.GetType().GetCustomAttribute(attributeType);
            
            return attribute is null;
        }
    }
}