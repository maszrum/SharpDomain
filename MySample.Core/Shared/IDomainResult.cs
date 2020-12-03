using System.Collections.Generic;
using MediatR;

namespace MySample.Core.Shared
{
    public interface IDomainResult<out T>
    {
        IReadOnlyList<INotification> Events { get; }
        T Model { get; }
    }
}