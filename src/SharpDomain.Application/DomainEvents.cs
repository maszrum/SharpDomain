using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using SharpDomain.ModelStateTracker;

namespace SharpDomain.Application
{
    internal class DomainEvents : IDomainEvents
    {
        private readonly IMediator _mediator;
        private readonly object _eventsLock = new();
        private readonly List<EventsBinding> _events = new();

        public DomainEvents(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishCollected(CancellationToken cancellationToken = default)
        {
            List<EventsBinding> eventsCopy;
            lock (_eventsLock)
            {
                eventsCopy = new List<EventsBinding>(_events);
                _events.Clear();
            }
            
            var bindingsOrdered = eventsCopy.OrderBy(b => b.Order);
            
            foreach (var binding in bindingsOrdered)
            {
                if (binding.EventWithModel is not null)
                {
                    await _mediator.Publish(binding.EventWithModel, cancellationToken);
                }
                await _mediator.Publish(binding.Event, cancellationToken);
            }
        }

        public IDomainEvents CollectFrom<TModel>(TModel model) where TModel : Aggregate
        {
            var bindings = model.DumpEvents()
                .Select(e =>
                {
                    var withModel = EventWithModel.CreateForLimitTypes(e, model);
                    return new EventsBinding(e, withModel);
                });
            
            lock (_eventsLock)
            {
                _events.AddRange(bindings);
            }
            
            return this;
        }

        public IDomainEvents Collect<TEvent>(TEvent @event) where TEvent : EventBase
        {
            lock (_eventsLock)
            {
                var binding = EventsBinding.WithoutModel(@event);
                _events.Add(binding);
            }
            
            return this;
        }

        public IDomainEvents Collect<TEvent, TModel>(TEvent @event, TModel model) 
            where TEvent : EventBase 
            where TModel : Aggregate
        {
            var eventWithModel = EventWithModel.CreateForLimitTypes(@event, model);
            var binding = new EventsBinding(@event, eventWithModel);
            
            lock (_eventsLock)
            {
                _events.Add(binding);
            }
            
            return this;
        }

        public IDisposable CollectPropertiesChange<TModel>(TModel model) where TModel : Aggregate
        {
            return new ModelChangesPublisher<TModel>(model)
                .OnCompare(changes =>
                {
                    var changedEvent = new ModelChanged<TModel>(changes.PropertiesChanged);
                    Collect(changedEvent, model);
                });
        }
        
        private class EventsBinding
        {
            public object Event { get; }
            public object? EventWithModel { get; }
            public int Order => Event.GetHashCode();

            public EventsBinding(object @event, object eventWithModel)
            {
                Event = @event;
                EventWithModel = eventWithModel;
            }

            private EventsBinding(object @event)
            {
                Event = @event;
                EventWithModel = default;
            }
            
            public static EventsBinding WithoutModel(object @event) => 
                new EventsBinding(@event);
        }
    }
}