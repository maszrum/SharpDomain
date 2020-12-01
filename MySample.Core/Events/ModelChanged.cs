using System.Collections.Generic;
using MediatR;

namespace MySample.Core.Events
{
    public class ModelChanged<T> : INotification
    {
        public ModelChanged(T model, List<string> propertiesChanged)
        {
            Model = model;
            _propertiesChanged = propertiesChanged;
        }
        
        private readonly List<string> _propertiesChanged;

        public T Model { get; }
        public IReadOnlyList<string> PropertiesChanged => _propertiesChanged;
    }
}