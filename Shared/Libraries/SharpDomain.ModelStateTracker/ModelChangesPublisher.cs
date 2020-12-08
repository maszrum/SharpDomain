using System;
using System.Threading.Tasks;

namespace SharpDomain.ModelStateTracker
{
    public class ModelChangesPublisher<T> : IDisposable where T : class
    {
        private Action<ComparisionResult<T>> _publishCallback;
        private readonly PropertiesTracker<T> _propertiesTracker;

        public ModelChangesPublisher(T model)
        {
            _propertiesTracker = new PropertiesTracker<T>(model);
            _propertiesTracker.SaveSnapshot();
        }
        
        public ModelChangesPublisher<T> OnCompare(Action<ComparisionResult<T>> callback)
        {
            if (_publishCallback != default)
            {
                throw new InvalidOperationException(
                    $"method {nameof(OnCompare)} method can be called only once");
            }
            
            _publishCallback = callback;
            
            return this;
        }
        
        public void Dispose()
        {
            if (_publishCallback == default)
            {
                throw new InvalidOperationException(
                    $"method {nameof(OnCompare)} was not called");
            }
            
            var comparisionResult = _propertiesTracker.CompareWithCurrentState();
            
            if (comparisionResult.PropertiesChanged.Count > 0)
            {
                _publishCallback(comparisionResult);
            }
        }
    }
}