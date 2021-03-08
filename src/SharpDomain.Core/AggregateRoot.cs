using System;

namespace SharpDomain.Core
{
    public abstract class AggregateRoot : Aggregate
    {
    }
    
    public abstract class AggregateRoot<TDefaultRules> : AggregateRoot
        where TDefaultRules : Rules, new()
    {
        protected void CheckRule(Func<TDefaultRules, RuleResult> ruleFunction) => 
            CheckRule<TDefaultRules>(ruleFunction);
        
        protected void CheckRules(params Func<TDefaultRules, RuleResult>[] ruleFunctions) =>
            CheckRules<TDefaultRules>(ruleFunctions);
    }
}