using System;

namespace SharpDomain.Errors
{
    public static class DomainError
    {
        public static DomainError<TException> FromException<TException>(TException exception) 
            where TException : Exception =>
            new DomainError<TException>(exception);
    }
    
    public class DomainError<TException> : ErrorBase where TException : Exception
    {
        public DomainError(TException exception)
        {
            Exception = exception;
            ExceptionType = exception.GetType();
        }

        public TException Exception { get; }
        public Type ExceptionType { get; }
        public override string Message => Exception.Message;
    }
}