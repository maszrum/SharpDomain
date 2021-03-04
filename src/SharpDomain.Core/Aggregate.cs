using System;

namespace SharpDomain.Core
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }
        
        private readonly Events _events = new();
        public IEvents Events => _events;

        protected static void CheckRule<TRules>(Func<TRules, RuleResult> ruleFunction) 
            where TRules : new()
        {
            var rule = new TRules();
            var ruleResult = ruleFunction(rule);
            
            if (!ruleResult.IsCorrect)
            {
                throw new InvalidOperationException(
                    $"{ruleResult.ErrorCode} : {ruleResult.ErrorMessage}"); // TODO
            }
        }
        
        protected static void CheckRules<TRules>(params Func<TRules, RuleResult>[] ruleFunctions) 
            where TRules : new()
        {
            foreach (var ruleFunction in ruleFunctions)
            {
                CheckRule(ruleFunction);
            }
        }
        
        public override int GetHashCode() => Id.GetHashCode();
    }
}