namespace SharpDomain.Core
{
    public interface IEvents
    {
        IEvents Append<TEvent>(TEvent @event) where TEvent : EventBase;
    }
}