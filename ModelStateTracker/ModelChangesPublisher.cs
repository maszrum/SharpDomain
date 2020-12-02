using System;
using System.Threading.Tasks;

namespace ModelStateTracker
{
    public class ModelChangesPublisher<T> : IAsyncDisposable where T : class
    {
        private Func<ComparisionResult<T>, Task> _publishCallback;
        private readonly PropertiesTracker<T> _propertiesTracker;

        public ModelChangesPublisher(T model)
        {
            _propertiesTracker = new PropertiesTracker<T>(model);
            _propertiesTracker.SaveSnapshot();
        }
        
        public ModelChangesPublisher<T> OnCompare(Func<ComparisionResult<T>, Task> callback)
        {
            if (_publishCallback != default)
            {
                throw new InvalidOperationException(
                    $"method {nameof(OnCompare)} method can be called only once");
            }
            
            _publishCallback = callback;
            
            return this;
        }
        
        public async ValueTask DisposeAsync()
        {
            if (_publishCallback == default)
            {
                throw new InvalidOperationException(
                    $"method {nameof(OnCompare)} was not called");
            }
            
            var comparisionResult = _propertiesTracker.CompareWithCurrentState();
            await _publishCallback(comparisionResult);
        }
    }
}