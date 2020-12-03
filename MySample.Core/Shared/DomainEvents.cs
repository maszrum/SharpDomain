using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MySample.Core.Shared
{
    internal class DomainEvents : IDomainEvents
    {
        private readonly IMediator _mediator;
        private readonly List<INotification> _events = new List<INotification>();

        public DomainEvents(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishCollected(CancellationToken cancellationToken = default)
        {
            foreach (var @event in _events)
            {
                await _mediator.Publish(@event, cancellationToken);
            }
        }

        public void Collect(INotification @event) => 
            _events.Add(@event);

        public void Collect(IEnumerable<INotification> events) => 
            _events.AddRange(events);
    }
}