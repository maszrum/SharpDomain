using System;
using Autofac;

namespace SharpDomain.IoC
{
    public abstract class SystemBuilder
    {
        protected ContainerBuilder ContainerBuilder { get; private set; } = new();
        
        public SystemBuilder UseContainerBuilder(ContainerBuilder containerBuilder)
        {
            ContainerBuilder = containerBuilder;
            return this;
        }
        
        public abstract SystemBuilder WireUpApplication();
        
        public SystemBuilder With(Action<ContainerBuilder> action)
        {
            action(ContainerBuilder);
            return this;
        }
        
        public IContainer Build() => ContainerBuilder.Build();
    }
}