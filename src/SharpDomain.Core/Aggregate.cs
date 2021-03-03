using System;

namespace SharpDomain.Core
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }
        
        private readonly Events _events = new();
        public IEvents Events => _events;

        protected static void CheckRule<TRules>(Func<TRules, RuleResult> ruleFunc) where TRules : new()
        {
            var rule = new TRules();
            var ruleResult = ruleFunc(rule);
            
            if (!ruleResult.IsCorrect)
            {
                throw new InvalidOperationException(
                    $"{ruleResult.ErrorCode} : {ruleResult.ErrorMessage}"); // TODO
            }
        }
        
        public override int GetHashCode() => Id.GetHashCode();
    }
}