using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using SharpDomain.IoC.EventHandler;

namespace SharpDomain.IoC.Persistence
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterPersistenceLayer(
            this ContainerBuilder containerBuilder,
            Assembly assembly)
        {
            return containerBuilder
                .RegisterPersistenceHandlers(assembly);
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
    }
}