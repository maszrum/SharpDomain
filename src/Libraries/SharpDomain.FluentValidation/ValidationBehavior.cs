using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SharpDomain.Application;

namespace SharpDomain.FluentValidation
{
    internal class ValidationBehavior<TRequest, TData> : IPipelineBehavior<TRequest, Response<TData>> 
        where TRequest : notnull
        where TData : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Response<TData>> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<Response<TData>> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = new List<ValidationFailure>();
            
            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(context, cancellationToken);
                
                if (validationResult.Errors.Any())
                {
                    failures.AddRange(validationResult.Errors);
                }
            }
            
            if (failures.Any())
            {
                return failures.ToError();
            }
            
            return await next();
        }
    }
}