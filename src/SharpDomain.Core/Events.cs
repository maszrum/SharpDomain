using System.Collections.Generic;

namespace SharpDomain.Core
{
    public class Events : List<EventBase>, IEvents
    {
        public void Add(params EventBase[] events)
        {
            AddRange(events);
        }
        
        public IReadOnlyList<EventBase> Dump()
        {
            var events = new List<EventBase>(this);
            Clear();
            return events;
        }
    }
}