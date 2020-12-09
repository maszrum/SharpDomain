using System;
using System.Runtime.InteropServices.ComTypes;

namespace SharpDomain.ModelStateTracker
{
    public class PropertiesTracker<T> where T : class
    {
        private readonly T _model;
        
        private PropertiesSnapshot _snapshot;

        public PropertiesTracker(T model)
        {
            _model = model;
        }

        public bool SnapshotCreated => _snapshot != default;
        
        public void SaveSnapshot()
        {
            _snapshot = CreateModelSnapshot();
        }
        
        public ComparisionResult<T> CompareWithCurrentState()
        {
            if (_snapshot == default)
            {
                throw new InvalidOperationException(
                    $"snapshot was not taken, use {nameof(SaveSnapshot)} method");
            }
            
            var currentStateSnapshot = CreateModelSnapshot();
            
            var propertiesChanged = currentStateSnapshot.GetDifferingProperties(_snapshot);
            var result = new ComparisionResult<T>(_model, propertiesChanged);
            
            return result;
        }
        
        private PropertiesSnapshot CreateModelSnapshot()
        {
            var factory = new PropertiesSnapshotFactory<T>(_model);
            return factory.Create();
        }
    }
}