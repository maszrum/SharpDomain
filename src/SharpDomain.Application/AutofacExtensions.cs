using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using SharpDomain.Core;
using SharpDomain.EventHandlerRegistration;

namespace SharpDomain.Application
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterApplicationLayer(
            this ContainerBuilder containerBuilder, 
            Assembly assembly,
            Action<ApplicationLayerConfiguration> configurationAction)
        {
            var configuration = new ApplicationLayerConfiguration();
            configurationAction(configuration);
            
            var legalityChecker = new DependencyLegalityChecker(configuration);
            containerBuilder
                .RegisterInstance(legalityChecker)
                .AsSelf();
            
            containerBuilder.RegisterBuildCallback(context =>
            {
                var checker = context.Resolve<DependencyLegalityChecker>();
                
                foreach (var registration in context.ComponentRegistry.Registrations)
                {
                    var serviceType = registration.Activator.LimitType;
                    checker.ThrowIfIllegalDependency(serviceType);
                }
            });
            
            return RegisterApplicationLayer(containerBuilder, assembly);
        }
        
        public static ContainerBuilder RegisterApplicationLayer(
            this ContainerBuilder containerBuilder,
            Assembly assembly)
        {
            return containerBuilder
                .RegisterMediatR()
                .RegisterRequestHandlers(assembly);
        }
        
        private static ContainerBuilder RegisterMediatR(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            containerBuilder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                
                return t => EventHandlerHelper.IsEventHandler(t) 
                    ? c.ResolveEventHandlers(t) 
                    : c.Resolve(t);
            });
            
            // ensure order: behaviors, exception handlers, exception actions
            containerBuilder
                .RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();
            
            containerBuilder
                .RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterRequestHandlers(
            this ContainerBuilder containerBuilder,
            Assembly assembly)
        {
            static bool IsRequestHandler(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

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
    }
}