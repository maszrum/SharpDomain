using System;

namespace SharpDomain.Core
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }
        
        private readonly Events _events = new();
        public IEvents Events => _events;

        public override int GetHashCode() => Id.GetHashCode();
    }
}