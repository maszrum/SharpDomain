using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;

namespace SharpDomain.AutoTransaction
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAutoTransaction(this ContainerBuilder containerBuilder, params Assembly[] persistenceAssemblies)
        {
            containerBuilder
                .RegisterGeneric(typeof(TransactionsBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            foreach (var assembly in persistenceAssemblies)
            {
                RegisterAutoTransactionHandler(containerBuilder, assembly);
            }
            
            return containerBuilder;
        }
        
        private static void RegisterAutoTransactionHandler(this ContainerBuilder containerBuilder,
            Assembly assembly)
        {
            static bool IsTransactionHandler(Type type)
            {
                if (!type.IsGenericType)
                {
                    return false;
                }
                var baseType = type.BaseType;
                if (baseType is null || !baseType.IsGenericType)
                {
                    return false;
                }
                return baseType.GetGenericTypeDefinition() == typeof(TransactionHandler<,>);
            }
            
            var handlers = assembly.DefinedTypes.Where(IsTransactionHandler).ToArray();

            foreach (var handler in handlers)
            {
                containerBuilder
                    .RegisterGeneric(handler)
                    .As(typeof(TransactionHandler<,>))
                    .InstancePerDependency();
            }
        }
    }
}