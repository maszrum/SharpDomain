using System;
using System.Collections.Generic;
using System.Reflection;

namespace MySample.Application
{
    public class ApplicationLayerConfiguration
    {
        public bool ForbidMediatorInHandlers { get; set; }
        
        public IReadOnlyList<Assembly> AssembliesThatHavePermittedWriteRepositories { get; private set; } = Array.Empty<Assembly>();
        
        public void PermitWriteRepositoriesInHandlersOnlyIn(params Assembly[] assemblies)
        {
            AssembliesThatHavePermittedWriteRepositories = assemblies;
        }
    }
}