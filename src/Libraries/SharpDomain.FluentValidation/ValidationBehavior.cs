using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace SharpDomain.FluentValidation
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
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
                throw new ValidationException(failures);
            }
            
            return await next();
        }
    }
}