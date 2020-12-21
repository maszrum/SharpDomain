using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using SharpDomain.Core;
using SharpDomain.IoC.EventHandler;

namespace SharpDomain.IoC.Core
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterDomainLayer(
            this ContainerBuilder containerBuilder, 
            Assembly assembly)
        {
            return containerBuilder
                .RegisterEventHandlers(assembly)
                .RegisterDomainEvents();
        }
        
        private static ContainerBuilder RegisterEventHandlers(
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
                    .AsDomainEventHandler(notificationHandlerType)
                    .InstancePerDependency();
            }
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterDomainEvents(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DomainEvents>()
                .As<IDomainEvents>()
                .InstancePerDependency();
            
            return containerBuilder;
        }
    }
}