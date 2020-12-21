using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using SharpDomain.Errors;

namespace SharpDomain.FluentValidation
{
    internal static class ValidationFailuresExtension
    {
        public static ValidationError ToError(this IEnumerable<ValidationFailure> failures)
        {
            var failuresConverted = failures
                .Select(f => new ValidationErrorFailure(f.PropertyName, f.ErrorMessage));
            
            return new ValidationError(failuresConverted);
        }
    }
}