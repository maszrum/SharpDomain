namespace SharpDomain.Core
{
    public class RuleResult
    {
        private RuleResult(string errorMessage, string errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public string ErrorMessage { get; }
        
        public string ErrorCode { get; }
        
        public bool IsCorrect => string.IsNullOrEmpty(ErrorMessage);
        
        public static RuleResult Correct() => 
            new(string.Empty, string.Empty);

        public static RuleResult Error(string message) => 
            new(message, string.Empty);
        
        public static RuleResult Error(string message, string errorCode) => 
            new(message, errorCode);
    }
}