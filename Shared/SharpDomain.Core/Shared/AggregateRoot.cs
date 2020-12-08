using System;
using MediatR;

namespace SharpDomain.Core.Shared
{
    public abstract class AggregateRoot<T> where T : AggregateRoot<T>
    {
        public Guid Id { get; protected set; }
        
        protected IDomainResult<T> Event(INotification @event) => new EventsResult<T>((this as T)!, @event);

        protected IDomainResult<T> Events(params INotification[] events) => new EventsResult<T>((this as T)!, events);

        protected IDomainResult<T> NoEvents() => new EmptyResult<T>((this as T)!);
        
        protected static IDomainResult<T> Event(INotification @event, T model) => new EventsResult<T>(model, @event);
    }
}