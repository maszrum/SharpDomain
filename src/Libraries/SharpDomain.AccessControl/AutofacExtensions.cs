using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;

namespace SharpDomain.AccessControl
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAuthorization(this ContainerBuilder containerBuilder, Assembly applicationAssembly)
        {
            return containerBuilder
                .RegisterConfigurationsContainer()
                .ConfigureConfigurationsContainerOnBuild(applicationAssembly)
                .RegisterRequirementsFromAssembly(applicationAssembly)
                .RegisterAuthorizationBehavior();
        }

        private static ContainerBuilder RegisterConfigurationsContainer(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<AuthorizablesConfigurations>()
                .AsSelf()
                .SingleInstance();

            return containerBuilder;
        }

        private static ContainerBuilder ConfigureConfigurationsContainerOnBuild(
            this ContainerBuilder containerBuilder,
            Assembly applicationAssembly)
        {
            static bool IsAuthorizable(Type t) =>
                t.GetInterfaces().Contains(typeof(IAuthorizable));

            containerBuilder.RegisterBuildCallback(container =>
            {
                using var scope = container.BeginLifetimeScope();

                var authorizables = applicationAssembly.GetTypes().Where(IsAuthorizable);
                var authorizablesConfigurations = scope.Resolve<AuthorizablesConfigurations>();

                foreach (var authorizableType in authorizables)
                {
                    (var requestType, var responseType) = RequestTypeHelper.GetRequestAndResponseTypes(authorizableType);
                    var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);

                    var requestHandlerInstance = (IAuthorizable)scope.Resolve(requestHandlerType);

                    var configuration = new AuthorizationConfiguration();
                    requestHandlerInstance.ConfigureAuthorization(configuration);

                    authorizablesConfigurations.Register(requestType, configuration);
                }
            });

            return containerBuilder;
        }

        private static ContainerBuilder RegisterRequirementsFromAssembly(
            this ContainerBuilder containerBuilder, 
            Assembly assembly)
        {
            static bool IsRequirement(Type type) => typeof(IAuthorizationRequirement).IsAssignableFrom(type);

            var requirementTypes = assembly.GetTypes().Where(IsRequirement);

            foreach (var requirementType in requirementTypes)
            {
                containerBuilder.RegisterType(requirementType)
                    .AsSelf()
                    .InstancePerLifetimeScope();
            }

            return containerBuilder;
        }

        private static ContainerBuilder RegisterAuthorizationBehavior(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(AuthorizationPipelineBehavior<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }
    }
}
