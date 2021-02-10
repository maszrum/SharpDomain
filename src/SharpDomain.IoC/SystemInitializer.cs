using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.IoC
{
    public abstract class SystemInitializer : INotificationHandler<InitializeNotification>
    {
        protected abstract Task InitializeIfNeed();
        protected abstract Task InitializeForcefully();
        
        public Task Handle(InitializeNotification notification, CancellationToken cancellationToken) =>
            notification.InitializationType switch
            {
                InitializationType.IfNeed => InitializeIfNeed(),
                InitializationType.Forcefully => InitializeForcefully(),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
