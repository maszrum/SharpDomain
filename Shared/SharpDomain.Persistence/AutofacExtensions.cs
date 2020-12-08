using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper.Configuration;
using MediatR;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterPersistenceLayer(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterPersistenceHandlers()
                .RegisterMappers();
        }
        
        private static ContainerBuilder RegisterPersistenceHandlers(this ContainerBuilder containerBuilder)
        {
            static bool IsNotificationHandler(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));
            
            var assembly = typeof(MyModelEntity).GetTypeInfo().Assembly;

            var notificationHandlerTypes = assembly.DefinedTypes
                .Where(IsNotificationHandler)
                .Select(t => (Type)t)
                .ToArray();
            
            containerBuilder
                .RegisterTypes(notificationHandlerTypes)
                .InstancePerDependency()
                .AsImplementedInterfaces();
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterMappers(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterBuildCallback(context =>
            {
                var assembly = typeof(AutofacExtensions).GetTypeInfo().Assembly;
                
                var mappings = context.Resolve<MapperConfigurationExpression>();
                mappings.AddMaps(assembly);
            });
            
            return containerBuilder;
        }
    }
}