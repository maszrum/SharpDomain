using System;
using MediatR;

namespace SharpDomain.Core
{
    internal static class EventWithModel
    {
        public static object CreateForLimitTypes(EventBase @event, Aggregate model)
        {
            var eventType = @event.GetType();
            var modelType = model.GetType();
            
            var ewmType = typeof(EventWithModel<,>).MakeGenericType(eventType, modelType);
            
            return Activator.CreateInstance(ewmType, @event, model);
        }
    }
    
    public class EventWithModel<TEvent, TModel> : INotification
        where TEvent : EventBase
        where TModel : Aggregate
    {
        public EventWithModel(TEvent @event, TModel model)
        {
            Event = @event;
            Model = model;
        }

        public TModel Model { get; }
        
        public TEvent Event { get; }
    }
}