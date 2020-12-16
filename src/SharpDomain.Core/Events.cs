using System.Collections.Generic;

namespace SharpDomain.Core
{
    public class Events : List<EventBase>, IEvents
    {
        public IEvents Append<TEvent>(TEvent @event) where TEvent : EventBase
        {
            Add(@event);
            
            return this;
        }
        
        public IReadOnlyList<EventBase> Dump()
        {
            var events = new List<EventBase>(this);
            Clear();
            return events;
        }
    }
}