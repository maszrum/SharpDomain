using System;

namespace SharpDomain.Core.ModelStateTracking
{
    public static class AggregateExtensions
    {
        public static ModelChanged<T> CaptureChangedEvent<T>(this T model, Action<T> modelAction) where T : Aggregate
        {
            var tracker = new PropertiesTracker<T>(model);
            tracker.SaveSnapshot();
            
            modelAction(model);
            
            var comparisionResult = tracker.CompareWithCurrentState();
            var changedEvent = new ModelChanged<T>(comparisionResult.PropertiesChanged);
            
            return changedEvent;
        }
    }
}