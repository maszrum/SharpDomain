using System;

namespace MySample.Core.Models
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
    }
}