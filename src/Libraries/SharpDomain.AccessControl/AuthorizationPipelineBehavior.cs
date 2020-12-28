using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.AccessControl
{
    internal class AuthorizationPipelineBehavior<TRequest, TData> : IPipelineBehavior<TRequest, Response<TData>>
        where TRequest : notnull
        where TData : class
    {
        private readonly AuthorizablesConfigurations _authorizablesConfigurations;
        private readonly ServiceFactory _serviceFactory;

        public AuthorizationPipelineBehavior(
            AuthorizablesConfigurations authorizablesConfigurations, 
            ServiceFactory serviceFactory)
        {
            _authorizablesConfigurations = authorizablesConfigurations;
            _serviceFactory = serviceFactory;
        }

        public async Task<Response<TData>> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<Response<TData>> next)
        {
            if (_authorizablesConfigurations.TryGet(request.GetType(), out var authorizationConfiguration) && 
                authorizationConfiguration.RequirementTypes.Count > 0)
            {
                var accessGranted = authorizationConfiguration.GrantCondition switch
                {
                    GrantCondition.WhenAllRequirements => await HandleWhenAllRequirements(authorizationConfiguration),
                    GrantCondition.WhenAnyRequirement => await HandleWhenAnyRequirement(authorizationConfiguration),
                    _ => throw new Exception($"ivalid value of {nameof(GrantCondition)} type")
                };

                if (!accessGranted)
                {
                    return new AuthorizationError("access denied");
                }
            }

            return await next();
        }

        private async Task<bool> HandleWhenAllRequirements(AuthorizationConfiguration configuration)
        {
            foreach (var requirementType in configuration.RequirementTypes)
            {
                var requirement = (IAuthorizationRequirement)_serviceFactory(requirementType);

                var context = new AuthorizationContext();
                await requirement.Handle(context);

                if (!context.AccessGranted)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> HandleWhenAnyRequirement(AuthorizationConfiguration configuration)
        {
            var context = new AuthorizationContext();

            foreach (var requirementType in configuration.RequirementTypes)
            {
                var requirement = (IAuthorizationRequirement)_serviceFactory(requirementType);

                await requirement.Handle(context);

                if (context.AccessGranted)
                {
                    return true;
                }
            }

            return false;
        }
    }
}