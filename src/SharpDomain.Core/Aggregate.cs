using System;
using System.Collections.Generic;

namespace SharpDomain.Core
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }
        
        private readonly Events _events = new();
        protected IEvents Events => _events;
        
        public IReadOnlyList<EventBase> DumpEvents() => _events.Dump();

        public override int GetHashCode() => Id.GetHashCode();
    }
}