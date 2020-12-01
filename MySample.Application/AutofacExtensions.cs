using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MySample.Core;

namespace MySample.Application
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterApplicationLayer(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterMediatR()
                .RegisterRequestHandlers()
                .RegisterNotificationHandlers();
        }
        
        private static ContainerBuilder RegisterMediatR(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            containerBuilder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>(); 
                return t => c.Resolve(t);
            });
            
            containerBuilder.RegisterBuildCallback(context =>
            {
                var mediator = context.Resolve<IMediator>();
                DomainEvents.Init(mediator);
            });
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterRequestHandlers(this ContainerBuilder containerBuilder)
        {
            static bool IsRequestHandler(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
            
            var assembly = typeof(AutofacExtensions).GetTypeInfo().Assembly;

            var requestHandlerTypes = assembly.DefinedTypes
                .Where(IsRequestHandler)
                .Select(t => (Type)t)
                .ToArray();
            
            containerBuilder
                .RegisterTypes(requestHandlerTypes)
                .InstancePerDependency()
                .AsImplementedInterfaces();
            
            return containerBuilder;
        } 
        
        private static ContainerBuilder RegisterNotificationHandlers(this ContainerBuilder containerBuilder)
        {
            static bool IsNotificationHandler(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));
            
            var assembly = typeof(AutofacExtensions).GetTypeInfo().Assembly;

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