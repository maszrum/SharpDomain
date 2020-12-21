namespace SharpDomain.Errors
{
    public class AuthenticationError : ErrorBase
    {
        public AuthenticationError(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}