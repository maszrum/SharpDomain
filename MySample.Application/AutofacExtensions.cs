using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using MySample.Core.Shared;

namespace MySample.Application
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterApplicationLayer(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterMediatR()
                .RegisterRequestHandlers()
                .RegisterNotificationHandlers()
                .RegisterAutomapper()
                .RegisterMappers();
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
        
        private static ContainerBuilder RegisterAutomapper(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<MapperConfigurationExpression>()
                .AsSelf()
                .SingleInstance();
            
            containerBuilder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var configExpression = context.Resolve<MapperConfigurationExpression>();
                return new MapperConfiguration(configExpression);
            })
                .AsSelf()
                .SingleInstance();
            
            containerBuilder.Register(c =>
            {
               var context = c.Resolve<IComponentContext>();
               var config = context.Resolve<MapperConfiguration>();
               return config.CreateMapper(context.Resolve);
            })
                .As<IMapper>()
                .InstancePerLifetimeScope();
            
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