using System;

namespace SharpDomain.Core
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }
        
        private readonly Events _events = new();
        public IEvents Events => _events;

        protected static void CheckRule<TRules>(Func<TRules, RuleResult> ruleFunction) 
            where TRules : Rules, new()
        {
            var rule = new TRules();
            var ruleResult = ruleFunction(rule);
            
            if (!ruleResult.IsCorrect)
            {
                throw new BrokenRuleException(
                    ruleResult.ErrorMessage, ruleResult.ErrorCode);
            }
        }
        
        protected static void CheckRules<TRules>(params Func<TRules, RuleResult>[] ruleFunctions) 
            where TRules : Rules, new()
        {
            foreach (var ruleFunction in ruleFunctions)
            {
                CheckRule(ruleFunction);
            }
        }
        
        public override int GetHashCode() => Id.GetHashCode();
    }
    
    public abstract class Aggregate<TDefaultRules> : Aggregate 
        where TDefaultRules : Rules, new()
    {
        protected void CheckRule(Func<TDefaultRules, RuleResult> ruleFunction) => 
            CheckRule<TDefaultRules>(ruleFunction);
        
        protected void CheckRules(params Func<TDefaultRules, RuleResult>[] ruleFunctions) =>
            CheckRules<TDefaultRules>(ruleFunctions);
    }
}