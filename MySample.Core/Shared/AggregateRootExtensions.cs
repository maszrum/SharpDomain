using System;
using ModelStateTracker;
using MySample.Core.Events;

namespace MySample.Core.Shared
{
    public static class AggregateRootExtensions
    {
        public static IAsyncDisposable PublishChanges<T>(this T model) where T : AggregateRoot<T>
        {
            var publisher = new ModelChangesPublisher<T>(model)
                .OnCompare(changes =>
                {
                    var @event = new ModelChanged<T>(changes.Model, changes.PropertiesChanged);
                    return DomainEvents.Publish(@event);
                });
            
            return publisher;
        }
    }
}