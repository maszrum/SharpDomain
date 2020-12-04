using System;
using System.Linq;
using MediatR;

namespace SharpDomain.Application
{
    internal class DependencyLegalityChecker
    {
        private readonly ApplicationLayerConfiguration _configuration;

        public DependencyLegalityChecker(ApplicationLayerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ThrowIfIllegalDependency(Type serviceType)
        {
            var isCheckableType = 
                IsRequestHandler(serviceType) || 
                IsEventHandler(serviceType) && !IsInPermittedAssembly(serviceType);
            
            if (!isCheckableType)
            {
                return;
            }
            
            if (_configuration.ForbidMediatorInHandlers)
            {
                var constructorParameters = GetConstructorParameters(serviceType);
                if (IsAnyMediator(constructorParameters))
                {
                    throw new InvalidOperationException(
                        $"mediator cannot be injected into command, query or event handler ({serviceType.FullName})");
                }
            }
            
            if (_configuration.AssembliesThatHavePermittedWriteRepositories.Any())
            {
                var constructorParameters = GetConstructorParameters(serviceType);
                if (IsAnyWriteRepository(constructorParameters))
                {
                    throw new InvalidOperationException(
                        $"write repositories cannot be injected into command, query or event handler ({serviceType.FullName})");
                }
            }
        }

        private bool IsInPermittedAssembly(Type type) => 
            _configuration.AssembliesThatHavePermittedWriteRepositories.Contains(type.Assembly);

        private static Type[] GetConstructorParameters(Type type) => 
            type.GetConstructors()
                .SelectMany(ctor => ctor.GetParameters())
                .Select(pi => pi.ParameterType)
                .ToArray();
        
        private static bool IsRequestHandler(Type type) =>
            type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i =>
                {
                    var gtd = i.GetGenericTypeDefinition();
                    return gtd == typeof(IRequestHandler<>) || gtd == typeof(IRequestHandler<,>);
                });

        private static bool IsEventHandler(Type type) =>
            type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));
        
        private static bool IsAnyWriteRepository(params Type[] types) =>
            types
                .Select(t => t.Name.ToLowerInvariant())
                .Any(tn => tn.Contains("repo") && tn.Contains("write"));
            
        private static bool IsAnyMediator(params Type[] types) =>
            types.Any(t => typeof(ISender).IsAssignableFrom(t) || typeof(IPublisher).IsAssignableFrom(t));
    }
}