using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpDomain.Core.ModelStateTracking
{
    internal class PropertiesSnapshotFactory<T> where T : class
    {
        private static PropertiesCache<T>? _propertiesCache;
        
        private readonly T _model;

        public PropertiesSnapshotFactory(T model)
        {
            _propertiesCache ??= new PropertiesCache<T>();

            _model = model;
        }
        
        public PropertiesSnapshot Create()
        {
            var snapshot = new PropertiesSnapshot();
            
            var properties = GetPropertiesOfModel();

            foreach (var pi in properties)
            {
                var propertyName = pi.Name;
                var propertyValue = pi.GetValue(_model);
                
                snapshot[propertyName] = propertyValue?.GetHashCode();
            }
            
            return snapshot;
        }
        
        private static IEnumerable<PropertyInfo> GetPropertiesOfModel()
        {
            if (_propertiesCache is null)
            {
                throw new NullReferenceException(
                    $"{nameof(_propertiesCache)} is null");
            }
            
            if (_propertiesCache.IsEmpty)
            {
                var properties = typeof(T)
                    .GetProperties()
                    .Where(pi => pi.GetGetMethod() != default)
                    .ToArray();

                foreach (var pi in properties)
                {
                    _propertiesCache.Add(pi);
                }
            }
            
            return _propertiesCache.GetCachedProperties();
        }
    }
}