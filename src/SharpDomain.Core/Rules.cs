using System.Runtime.CompilerServices;

namespace SharpDomain.Core
{
    public abstract class Rules
    {
        protected RuleResult Error(string message, string errorCode) => 
            RuleResult.Error(message, errorCode);
        
        protected RuleResult Correct() => 
            RuleResult.Correct();
        
        protected SuccessIfBuilder SuccessIf(bool condition) => 
            new(condition);

        protected class SuccessIfBuilder
        {
            private readonly bool _condition;
            
            public SuccessIfBuilder(bool condition)
            {
                _condition = condition;
            }
            
            public RuleResult OtherwiseError(string message, [CallerMemberName] string errorCode = "")
            {
                return _condition
                    ? RuleResult.Correct() 
                    : RuleResult.Error(message, errorCode);
            }
        }
    }
}