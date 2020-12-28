namespace SharpDomain.Responses
{
    public class UserError : ErrorBase
    {
        public UserError(string message)
        {
            Message = message;
        }
        
        public override string Message { get; }
    }
}