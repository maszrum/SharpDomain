using Autofac;
using MediatR;
using MySample.Core;

namespace MySample.Application
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterApplicationLayer(this ContainerBuilder containerBuilder)
        {
            return containerBuilder
                .RegisterMediatR();
        }
        
        private static ContainerBuilder RegisterMediatR(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            containerBuilder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>(); 
                return t => c.Resolve(t);
            });
            
            containerBuilder.RegisterBuildCallback(context =>
            {
                var mediator = context.Resolve<IMediator>();
                DomainEvents.Init(mediator);
            });
            
            return containerBuilder;
        }
    }
}