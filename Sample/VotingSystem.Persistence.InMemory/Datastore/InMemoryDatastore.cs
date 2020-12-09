using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.InMemory.Datastore
{
    internal class InMemoryDatastore
    {
        public InMemoryDatastore()
        {
            _myEntityStore = new EntityDatastore<QuestionEntity>();
            
            _dataStores = new IEntityDatastore[] 
            {
                _myEntityStore
            };
        }
        
        private readonly EntityDatastore<QuestionEntity> _myEntityStore;
        private readonly IEntityDatastore[] _dataStores;
        
        public IDictionary<Guid, QuestionEntity> MyModels => _myEntityStore.Models;
        
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