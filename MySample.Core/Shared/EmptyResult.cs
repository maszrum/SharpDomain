using System;
using System.Collections.Generic;
using MediatR;

namespace MySample.Core.Shared
{
    internal class EmptyResult<T> : IDomainResult<T>
    {
        public EmptyResult(T model)
        {
            Model = model;
            Events = Array.Empty<INotification>();
        }

        public IReadOnlyList<INotification> Events { get; }
        public T Model { get; }
    }
}