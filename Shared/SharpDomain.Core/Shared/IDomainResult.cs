using System.Collections.Generic;
using MediatR;

namespace SharpDomain.Core.Shared
{
    public interface IDomainResult<out T>
    {
        IReadOnlyList<INotification> Events { get; }
        T Model { get; }
    }
}