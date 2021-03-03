using System;

namespace SharpDomain.Core
{
    public abstract class DomainException : Exception
    {
        public string? ErrorCode { get; }
        
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, string errorCode) 
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}