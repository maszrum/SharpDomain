using System.Collections.Generic;

namespace SharpDomain.Core
{
    public interface IEvents
    {
        void Add(params EventBase[] events);
        IReadOnlyList<EventBase> Dump();
    }
}