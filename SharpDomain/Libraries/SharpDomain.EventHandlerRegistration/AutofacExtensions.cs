using System;
using Autofac;
using Autofac.Builder;
using MediatR;

namespace SharpDomain.EventHandlerRegistration
{
    public static class AutofacExtensions
    {
        private const string PersistenceKey = "Persistence";
        private const string DomainKey = "Domain";
        
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
            AsDomainEventHandler<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, 
                Type type)
        {
            return registrationBuilder.AsEventHandler(type, DomainKey);
        }
        
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
            AsPersistenceEventHandler<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, 
                Type type)
        {
            return registrationBuilder.AsEventHandler(type, PersistenceKey);
        }
        
        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> 
            AsEventHandler<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, 
                Type type,
                string key)
        {
            var interfaceType = type.GetInterface(typeof(INotificationHandler<>).Name);
            var requestType = interfaceType.GetGenericArguments()[0];
            var notificationHandlerType = typeof(INotificationHandler<>).MakeGenericType(requestType);
            return registrationBuilder.Keyed(key, notificationHandlerType);
        }
        
        public static object ResolveEventHandlers(this IComponentContext context, Type enumerableType)
        {
            var serviceType = enumerableType.GetGenericArguments()[0];
            
            var persistenceHandlers = (object[])context.ResolveKeyed(PersistenceKey, enumerableType);
            var domainHandlers = (object[])context.ResolveKeyed(DomainKey, enumerableType);
            
            var array = Array.CreateInstance(serviceType, persistenceHandlers.Length + domainHandlers.Length);
            
            persistenceHandlers.CopyTo(array, 0);
            domainHandlers.CopyTo(array, persistenceHandlers.Length);
            
            return array;
        }
    }
}