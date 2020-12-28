namespace SharpDomain.Responses
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