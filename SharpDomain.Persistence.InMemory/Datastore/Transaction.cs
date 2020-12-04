using System;
using System.Threading.Tasks;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    public class Transaction : IAsyncDisposable
    {
        private readonly InMemoryDatastore _datastore;
        
        public Transaction(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }
        
        public Task Commit() => 
            _datastore.Commit();

        public Task Rollback() =>
            _datastore.Rollback();

        public ValueTask DisposeAsync() => default;
    }
}