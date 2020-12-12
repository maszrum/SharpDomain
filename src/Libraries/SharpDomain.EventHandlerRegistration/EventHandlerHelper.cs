using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace SharpDomain.EventHandlerRegistration
{
    public static class EventHandlerHelper
    {
        public static bool IsEventHandler(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            
            var isEnumerable = type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            if (!isEnumerable)
            {
                return false;
            }
            
            var enumerableArg = type.GetGenericArguments()[0];
            if (!enumerableArg.IsGenericType)
            {
                return false;
            }
            
            return enumerableArg.GetGenericTypeDefinition() == typeof(INotificationHandler<>);
        }

        internal static Array ConcatArraysOfServices(Type serviceType, params object[][] arrays)
        {
            var servicesCount = arrays.Sum(a => a.Length);
            var array = Array.CreateInstance(serviceType, servicesCount);

            var copyToIndex = 0;
            foreach (var services in arrays)
            {
                if (services.Length > 0)
                {
                    services.CopyTo(array, copyToIndex);
                    copyToIndex += services.Length;
                }
            }

            return array;
        }
    }
}