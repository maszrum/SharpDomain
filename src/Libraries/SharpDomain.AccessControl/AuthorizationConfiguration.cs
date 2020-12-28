using System;
using System.Collections.Generic;

namespace SharpDomain.AccessControl
{
    internal enum GrantCondition
    {
        WhenAllRequirements,
        WhenAnyRequirement
    }

    public class AuthorizationConfiguration
    {
        internal GrantCondition GrantCondition { get; private set; } = GrantCondition.WhenAllRequirements;

        private readonly List<Type> _requirementTypes = new List<Type>();

        public IReadOnlyList<Type> RequirementTypes => _requirementTypes;

        public AuthorizationConfiguration UseRequirement<TRequirement>() 
            where TRequirement : IAuthorizationRequirement
        {
            _requirementTypes.Add(typeof(TRequirement));

            return this;
        }

        public AuthorizationConfiguration GrantAccessWhenAny()
        {
            GrantCondition = GrantCondition.WhenAnyRequirement;

            return this;
        }

        public AuthorizationConfiguration GrantAccessWhenAll()
        {
            GrantCondition = GrantCondition.WhenAllRequirements;

            return this;
        }
    }
}
