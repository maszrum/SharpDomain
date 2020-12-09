using System;
using System.Reflection;
using Autofac;
using VotingSystem.Persistence.InMemory.Datastore;

namespace VotingSystem.Persistence.InMemory
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterInMemoryPersistence(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterRepositories()
                .RegisterInMemoryDatasource();
        }
        
        private static ContainerBuilder RegisterRepositories(this ContainerBuilder containerBuilder)
        {
            static bool IsRepositoryType(Type t) => 
                t.Name.ToLowerInvariant().Contains("repository") && !t.IsAbstract;
            
            var assembly = typeof(AutofacExtensions).GetTypeInfo().Assembly;
            
            containerBuilder
                .RegisterAssemblyTypes(assembly)
                .Where(IsRepositoryType)
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterInMemoryDatasource(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<InMemoryDatastore>()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            return containerBuilder;
        }
    }
}