using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MySample.Persistence.Entities;

namespace MySample.Persistence
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterPersistenceLayer(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterPersistenceHandlers();
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
    }
}