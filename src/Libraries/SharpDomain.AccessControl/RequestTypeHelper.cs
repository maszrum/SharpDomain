using System;
using System.Linq;
using MediatR;

namespace SharpDomain.AccessControl
{
    internal static class RequestTypeHelper
    {
        public static (Type, Type) GetRequestAndResponseTypes(Type handlerType)
        {
            var handlerInterfaceType = handlerType.GetInterfaces()
                .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            if (handlerInterfaceType is null)
            {
                throw new InvalidOperationException(
                    $"specified type does not implement {typeof(IRequestHandler<,>).Name} interface");
            }

            var genericArgs = handlerInterfaceType.GetGenericArguments();

            return (genericArgs[0], genericArgs[1]);
        }
    }
}
