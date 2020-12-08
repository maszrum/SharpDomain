using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleDomain.Persistence.Entities;

namespace SampleDomain.Persistence.InMemory.Datastore
{
    internal class InMemoryDatastore
    {
        public InMemoryDatastore()
        {
            _myEntityStore = new EntityDatastore<MyModelEntity>();
            
            _dataStores = new IEntityDatastore[] 
            {
                _myEntityStore
            };
        }
        
        private readonly EntityDatastore<MyModelEntity> _myEntityStore;
        private readonly IEntityDatastore[] _dataStores;
        
        public IDictionary<Guid, MyModelEntity> MyModels => _myEntityStore.Models;
        
        public Task<InMemoryTransaction> BeginTransaction()
        {
            OnTransactionBegin();
            
            return Task.FromResult(new InMemoryTransaction(OnCommit, OnRollback, OnTransactionDispose));
        }
        
        private void OnTransactionBegin()
        {
            foreach (var datastore in _dataStores)
            {
                datastore.SetSourceToCopy();
            }
        }
        
        private void OnTransactionDispose()
        {
            foreach (var datastore in _dataStores)
            {
                datastore.SetSourceToOrigin();
            }
        }
            
        private Task OnCommit()
        {
            foreach (var datastore in _dataStores)
            {
                datastore.Commit();
            }
            
            return Task.CompletedTask;
        }
            
        private Task OnRollback()
        {
            foreach (var datastore in _dataStores)
            {
                datastore.Rollback();
            }
            
            return Task.CompletedTask;
        }
    }
}