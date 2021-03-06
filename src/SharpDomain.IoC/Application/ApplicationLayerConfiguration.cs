﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpDomain.IoC.Application
{
    public class ApplicationLayerConfiguration
    {
        public bool ForbidMediatorInHandlers { get; set; }
        
        public IReadOnlyList<Assembly> AssembliesThatHavePermittedWriteRepositories { get; private set; } = Array.Empty<Assembly>();
        
        public void ForbidWriteRepositoriesInHandlersExceptIn(params Assembly[] assemblies)
        {
            AssembliesThatHavePermittedWriteRepositories = assemblies;
        }
    }
}