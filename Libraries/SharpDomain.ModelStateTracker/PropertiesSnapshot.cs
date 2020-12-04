using System;
using System.Collections.Generic;

namespace SharpDomain.ModelStateTracker
{
    internal class PropertiesSnapshot
    {
        private readonly Dictionary<string, int?> _propertyValues = new Dictionary<string, int?>();

        public int? this[string propertyName]
        {
            get => _propertyValues[propertyName];
            set => _propertyValues[propertyName] = value;
        }
        
        public bool TryGetValue(string propertyName, out int? value) =>
            _propertyValues.TryGetValue(propertyName, out value);

        public IEnumerable<string> GetDifferingProperties(PropertiesSnapshot other)
        {
            var result = new List<string>();
            
            foreach (var (propertyName, propertyValue) in _propertyValues)
            {
                if (!other.TryGetValue(propertyName, out var otherValue))
                {
                    throw new InvalidOperationException(
                        "snapshots cotains different properties");
                }
                
                if (propertyValue.HasValue && otherValue.HasValue)
                {
                    if (propertyValue != otherValue)
                    {
                        result.Add(propertyName);
                    }
                }
                else if (propertyValue.HasValue != otherValue.HasValue)
                {
                    result.Add(propertyName);
                }
            }
            
            return result;
        }
    }
}