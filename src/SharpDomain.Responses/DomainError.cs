using System;

namespace SharpDomain.Responses
{
    public class DomainError : ErrorBase
    {
        public DomainError(Exception exception)
        {
            Exception = exception;
            ExceptionType = exception.GetType();
        }

        public Exception Exception { get; }
        public Type ExceptionType { get; }
        
        public override string Message => $"{ExceptionType.Name}: {Exception.Message}";
    }
}