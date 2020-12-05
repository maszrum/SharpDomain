using System;
using System.Threading.Tasks;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    public class Transaction : IAsyncDisposable
    {
        private readonly Func<Task> _commitAction;
        private readonly Func<Task> _rollbackAction;

        public Transaction(
            Func<Task> commitAction, 
            Func<Task> rollbackAction)
        {
            _commitAction = commitAction;
            _rollbackAction = rollbackAction;
        }
        
        public Task Commit() => 
            _commitAction();

        public Task Rollback() =>
            _rollbackAction();

        public ValueTask DisposeAsync() => default;
    }
}