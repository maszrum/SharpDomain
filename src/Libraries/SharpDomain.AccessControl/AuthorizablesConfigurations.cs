using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace SharpDomain.AccessControl
{
    internal class AuthorizablesConfigurations
    {
        private readonly Dictionary<Type, AuthorizationConfiguration> _configurations = new();

        public bool TryGet(Type requestType, out AuthorizationConfiguration configuration) =>
            _configurations.TryGetValue(requestType, out configuration);

        public void Register(Type requestType, AuthorizationConfiguration configuration) => 
            _configurations.Add(requestType, configuration);
    }
}
