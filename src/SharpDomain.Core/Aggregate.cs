using System;
using MediatR;

namespace SharpDomain.Core
{
    public abstract class Aggregate<T> : IEquatable<Aggregate<T>> where T : Aggregate<T>
    {
        public abstract Guid Id { get; }
        
        public bool Equals(Aggregate<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Aggregate<T>) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();
        
        protected IDomainResult<T> Event(INotification @event) => new EventsResult<T>((this as T)!, @event);

        protected IDomainResult<T> Events(params INotification[] events) => new EventsResult<T>((this as T)!, events);

        protected IDomainResult<T> NoEvents() => new EmptyResult<T>((this as T)!);
        
        protected static IDomainResult<T> Event(INotification @event, T model) => new EventsResult<T>(model, @event);
    }
}