namespace SharpDomain.Core
{
    public class BrokenRuleException : DomainException
    {
        public BrokenRuleException(string message, string errorCode) 
            : base(message, errorCode)
        {
        }
    }
}