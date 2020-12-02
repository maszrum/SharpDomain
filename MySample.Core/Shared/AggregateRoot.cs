using System;

namespace MySample.Core.Shared
{
    public abstract class AggregateRoot<T> where T : class
    {
        public Guid Id { get; protected set; }
    }
}