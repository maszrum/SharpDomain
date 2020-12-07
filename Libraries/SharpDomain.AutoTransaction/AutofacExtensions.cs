using Autofac;
using MediatR;

namespace SharpDomain.Transactions
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAutoTransaction(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterGeneric(typeof(TransactionsBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();
            
            return containerBuilder;
        }
    }
}