using System;
using SharpDomain.ModelStateTracker;

namespace SharpDomain.Core
{
    public static class AggregateRootExtensions
    {
        public static T CollectEvents<T>(this IDomainResult<T> domainResult, IDomainEvents domainEvents)
        {
            domainEvents.Collect(domainResult.Events);
            
            return domainResult.Model;
        }
        
        public static IDisposable CollectPropertiesChange<T>(this T model, IDomainEvents domainEvents) where T : Aggregate<T>
        {
            return new ModelChangesPublisher<T>(model)
                .OnCompare(changes =>
                {
                    var @event = new ModelChanged<T>(changes.Model, changes.PropertiesChanged);
                    domainEvents.Collect(@event);
                });
        }
        
        public static ModelChanged<T> CaptureChangedEvent<T>(this T model, Action<T> modelAction) where T : Aggregate<T>
        {
            var tracker = new PropertiesTracker<T>(model);
            tracker.SaveSnapshot();
            
            modelAction(model);
            
            var comparisionResult = tracker.CompareWithCurrentState();
            var changedEvent = new ModelChanged<T>(comparisionResult.Model, comparisionResult.PropertiesChanged);
            
            return changedEvent;
        }
    }
}