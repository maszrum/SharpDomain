﻿using System;
using System.Threading.Tasks;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    public class Transaction : IAsyncDisposable
    {
        private readonly Func<Task> _commitAction;
        private readonly Func<Task> _rollbackAction;
        private readonly Action _disposeAction;

        public Transaction(
            Func<Task> commitAction, 
            Func<Task> rollbackAction, 
            Action disposeAction)
        {
            _commitAction = commitAction;
            _rollbackAction = rollbackAction;
            _disposeAction = disposeAction;
        }
        
        public Task Commit() => 
            _commitAction();

        public Task Rollback() =>
            _rollbackAction();

        public ValueTask DisposeAsync()
        {
            _disposeAction();
            return default;
        }
    }
}