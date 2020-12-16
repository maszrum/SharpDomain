using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDomain.Core
{
    public interface IDomainEvents
    {
        IDomainEvents Collect<TEvent>(TEvent @event) 
            where TEvent : EventBase;
        
        IDomainEvents Collect<TEvent, TModel>(TEvent @event, TModel model) 
            where TEvent : EventBase 
            where TModel : Aggregate;
        
        IDomainEvents CollectFrom<TModel>(TModel model) 
            where TModel : Aggregate;
        
        IDisposable CollectPropertiesChange<TModel>(TModel model) 
            where TModel : Aggregate;
        
        Task PublishCollected(CancellationToken cancellationToken = default);
    }
}