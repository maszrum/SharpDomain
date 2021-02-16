using MediatR;

namespace SharpDomain.Core
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> 
        where TEvent : EventBase 
    {
    }
}