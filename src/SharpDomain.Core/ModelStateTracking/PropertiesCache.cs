using System.Collections.Generic;
using System.Reflection;

namespace SharpDomain.Core.ModelStateTracking
{
    internal class PropertiesCache<T> where T : class
    {
        private readonly Dictionary<string, PropertyInfo> _properties = new Dictionary<string, PropertyInfo>();
        private readonly object _lock = new object();
        
        public bool IsEmpty { get; private set; } = true;
        
        public void Add(PropertyInfo propertyInfo)
        {
            lock (_lock)
            {
                _properties.Add(propertyInfo.Name, propertyInfo);
            }
            
            IsEmpty = false;
        }
        
        public IEnumerable<PropertyInfo> GetCachedProperties()
        {
            IEnumerable<PropertyInfo> result;
            lock (_lock)
            {
                result = new List<PropertyInfo>(_properties.Values);
            }
            
            return result;
        }
    }
}