using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SharpDomain.Core
{
    public interface IDomainEvents
    {
        Task PublishCollected(CancellationToken cancellationToken = default);
        void Collect(INotification @event);
        void Collect(IEnumerable<INotification> events);
    }
}