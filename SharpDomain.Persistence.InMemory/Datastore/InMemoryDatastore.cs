using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InMemoryDatastore
    {
        public InMemoryDatastore()
        {
            _myEntityStore = new EntityDatastore<MyModelEntity>();
            
            _datastores = new IEntityDatastore[] 
            {
                _myEntityStore
            };
        }
        
        private readonly EntityDatastore<MyModelEntity> _myEntityStore;
        private readonly IEntityDatastore[] _datastores;
        
        public IDictionary<Guid, MyModelEntity> MyModels => _myEntityStore.Models;
        
        public Task<Transaction> BeginTransaction()
        {
            OnTransactionBegin();
            
            return Task.FromResult(new Transaction(OnCommit, OnRollback, OnTransactionDispose));
        }
        
        private void OnTransactionBegin()
        {
            foreach (var datastore in _datastores)
            {
                datastore.SetSourceToCopy();
            }
        }
        
        private void OnTransactionDispose()
        {
            foreach (var datastore in _datastores)
            {
                datastore.SetSourceToOrigin();
            }
        }
            
        private Task OnCommit()
        {
            foreach (var datastore in _datastores)
            {
                datastore.Commit();
            }
            
            return Task.CompletedTask;
        }
            
        private Task OnRollback()
        {
            foreach (var datastore in _datastores)
            {
                datastore.Rollback();
            }
            
            return Task.CompletedTask;
        }
    }
}