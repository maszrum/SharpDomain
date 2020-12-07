using System;
using SharpDomain.ModelStateTracker;
using SharpDomain.Core.Events;
using SharpDomain.Core.Shared;

namespace SharpDomain.Application.Shared
{
    internal static class AggregateRootExtensions
    {
        public static T CollectEvents<T>(this IDomainResult<T> domainResult, IDomainEvents domainEvents)
        {
            domainEvents.Collect(domainResult.Events);
            
            return domainResult.Model;
        }
        
        public static IDisposable CollectPropertiesChange<T>(this T model, IDomainEvents domainEvents) where T : AggregateRoot<T>
        {
            return new ModelChangesPublisher<T>(model)
                .OnCompare(changes =>
                {
                    var @event = new ModelChanged<T>(changes.Model, changes.PropertiesChanged);
                    domainEvents.Collect(@event);
                });
        }
    }
}