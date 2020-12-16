using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;

namespace SharpDomain.Persistence
{
    public abstract class InfrastructureHandler<TEvent, TModel> 
        : INotificationHandler<EventWithModel<TEvent, TModel>> 
        where TEvent : EventBase 
        where TModel : Aggregate
    {
        public Task Handle(EventWithModel<TEvent, TModel> notification, CancellationToken cancellationToken) => 
            Handle(notification.Event, notification.Model, cancellationToken);

        public abstract Task Handle(TEvent @event, TModel model, CancellationToken cancellationToken);
    }
}