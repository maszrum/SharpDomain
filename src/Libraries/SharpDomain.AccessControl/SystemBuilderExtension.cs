using Autofac;
using SharpDomain.IoC;

namespace SharpDomain.AccessControl
{
    public static class SystemBuilderExtension
    {
        public static SystemBuilder WithIdentityService<TIdentityService, TIdentity>(this SystemBuilder systemBuilder) 
            where TIdentityService : IIdentityService<TIdentity> 
            where TIdentity : IIdentity
        {
            systemBuilder.With(containerBuilder =>
            {
                containerBuilder
                    .RegisterType<TIdentityService>()
                    .AsSelf()
                    .As<IIdentityService<TIdentity>>()
                    .InstancePerLifetimeScope();
            });
            
            return systemBuilder;
        }
    }
}