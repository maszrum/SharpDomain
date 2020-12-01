using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySample.Core.Events;

namespace MySample.Core.Shared
{
    public class ModelModifier<T> : IAsyncDisposable where T : AggregateRoot<T>
    {
        private readonly T _model;
        private readonly List<string> _propertiesModified = new List<string>();

        public ModelModifier(T model)
        {
            _model = model;
        }
        
        public bool IsDisposed { get; private set; }

        public void AppendPropertyModified(string propertyName)
        {
            if (!_propertiesModified.Contains(propertyName))
            {
                _propertiesModified.Add(propertyName);
            }
        }
        
        public async ValueTask DisposeAsync()
        {
            IsDisposed = true;
            
            var @event = new ModelChanged<T>(_model, _propertiesModified);
            await DomainEvents.Publish(@event);
        }
    }
}