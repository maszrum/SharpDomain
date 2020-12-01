using System.Reflection;
using Autofac;

namespace MySample.Persistence.InMemory
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterInMemoryRepositories(this ContainerBuilder containerBuilder)
        {
            var assembly = typeof(AutofacExtensions).GetTypeInfo().Assembly;
            
            containerBuilder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.Contains("Repository") && !t.IsAbstract)
                .AsImplementedInterfaces();
            
            return containerBuilder;
        }
    }
}