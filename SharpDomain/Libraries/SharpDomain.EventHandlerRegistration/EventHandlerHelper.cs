using System;
using System.Collections.Generic;
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
    }
}