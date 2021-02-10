using Autofac;
using MediatR;

namespace SharpDomain.IoC
{
    public static class InitializeExtension
    {
        public static TBuilder Initialize<TBuilder>(this TBuilder systemBuilder, InitializationType initializationType) 
            where TBuilder : SystemBuilder
        {
            systemBuilder.OnBuild(context =>
            {
                var mediator = context.Resolve<IMediator>();
                return mediator.Publish(new InitializeNotification(initializationType));
            });
            
            return systemBuilder;
        }
    }
}