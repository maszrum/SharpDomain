using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MySample.Core
{
    // singleton
    public static class DomainEvents
    {
        private static IMediator? _mediator;
        
        public static void Init(IMediator mediator)
        {
            if (_mediator != default)
            {
                throw new InvalidOperationException(
                    "init method must be called only once");
            }

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        public static Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
            where TNotification : INotification =>
            _mediator!.Publish(notification, cancellationToken);
    }
}