using Autofac;

namespace SharpDomain.Core
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterDomainLayer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DomainEvents>()
                .As<IDomainEvents>()
                .InstancePerDependency();
            
            return containerBuilder;
        }
    }
}