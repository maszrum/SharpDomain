using Autofac;
using MySample.Core.Shared;

namespace MySample.Core
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