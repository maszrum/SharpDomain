using MediatR;

namespace SharpDomain.Core
{
    public abstract class AggregateRoot<T> : Aggregate<T> where T : Aggregate<T>
    {
    }
}