using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDomain.Errors
{
    public class ValidationError : ErrorBase
    {
        public IReadOnlyList<ValidationErrorFailure> Failures { get; }

        public ValidationError(IEnumerable<ValidationErrorFailure> failures)
        {
            Failures = failures as List<ValidationErrorFailure> ?? failures.ToList();
            if (Failures.Count == 0)
            {
                throw new ArgumentException(
                    "must contain at leat one element", nameof(failures));
            }
            
            Message = FormatMessage();
        }
        
        public override string Message { get; }
        
        private string FormatMessage() => 
            string.Join(Environment.NewLine, Failures);
    }
    
    public class ValidationErrorFailure
    {
        public ValidationErrorFailure(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public string PropertyName { get; }
        public string Message { get; }

        public override string ToString() => 
            $"{PropertyName}: {Message}";
    }
}