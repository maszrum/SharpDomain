using System.Collections.Generic;
using System.Linq;

namespace SharpDomain.Core
{
    public class ModelChanged<T> : EventBase where T : Aggregate
    {
        public ModelChanged(IEnumerable<string> propertiesChanged)
        {
            PropertiesChanged = propertiesChanged as IReadOnlyList<string> ?? propertiesChanged.ToList();
        }
        
        public ModelChanged(params string[] propertiesChanged)
        {
            PropertiesChanged = propertiesChanged;
        }
        
        public IReadOnlyList<string> PropertiesChanged { get; }
    }
}