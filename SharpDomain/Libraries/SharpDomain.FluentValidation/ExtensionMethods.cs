using System;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;

namespace SharpDomain.FluentValidation
{
    public static class ExtensionMethods
    {
        public static ContainerBuilder RegisterFluentValidation(
            this ContainerBuilder containerBuilder,
            Assembly applicationAssembly)
        {
            return containerBuilder
                .RegisterValidators(applicationAssembly)
                .RegisterValidationBehavior();
        }
        
        private static ContainerBuilder RegisterValidators(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            static bool IsValidatorType(Type t) =>
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
            
            var types = assembly.DefinedTypes
                .Where(IsValidatorType)
                .Select(t => t.AsType())
                .ToArray();
            
            containerBuilder
                .RegisterTypes(types)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            return containerBuilder;
        }
        
        private static ContainerBuilder RegisterValidationBehavior(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterGeneric(typeof(ValidationBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();
            
            return containerBuilder;
        }
    }
}