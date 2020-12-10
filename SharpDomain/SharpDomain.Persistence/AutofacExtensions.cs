using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper.Configuration;
using MediatR;
using SharpDomain.EventHandlerRegistration;

namespace SharpDomain.Persistence
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterPersistenceLayer(
            this ContainerBuilder containerBuilder,
            Assembly assembly)
        {
            return containerBuilder
                .RegisterPersistenceHandlers(assembly)
                .RegisterMappers(assembly);
        }
        
        private static ContainerBuilder RegisterPersistenceHandlers(
            this ContainerBuilder containerBuilder, 
            Assembly assembly)
        {
            static bool IsNotificationHandler(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

            var notificationHandlerTypes = assembly.DefinedTypes
                .Where(IsNotificationHandler)
                .Select(t => (Type)t)
                .ToArray();

            foreach (var notificationHandlerType in notificationHandlerTypes)
            {
                containerBuilder
                    .RegisterType(notificationHandlerType)
                    .AsPersistenceEventHandler(notificationHandlerType)
                    .InstancePerDependency();
            }
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterMappers(
            this ContainerBuilder containerBuilder, 
            Assembly assembly)
        {
            containerBuilder.RegisterBuildCallback(context =>
            {
                var mappings = context.Resolve<MapperConfigurationExpression>();
                mappings.AddMaps(assembly);
            });
            
            return containerBuilder;
        }
    }
}