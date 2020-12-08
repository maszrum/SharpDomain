using System.Collections.Generic;
using MediatR;

namespace SharpDomain.Core.Shared
{
    internal class EventsResult<T> : IDomainResult<T>
    {
        public EventsResult(T model, params INotification[] events)
        {
            Model = model;
            Events = events;
        }

        public T Model { get; }
        public IReadOnlyList<INotification> Events { get; }
    }
}