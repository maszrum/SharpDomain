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
        private readonly ModelDatastore<MyModelEntity> _myModelStore 
            = new ModelDatastore<MyModelEntity>();
        
        public IDictionary<Guid, MyModelEntity> MyModels => _myModelStore.Models;
        
        public Task<Transaction> BeginTransaction() =>
            Task.FromResult(new Transaction(Commit, Rollback));

        private Task Commit()
        {
            _myModelStore.Commit();
            
            return Task.CompletedTask;
        }
        
        private Task Rollback()
        {
            _myModelStore.Rollback();
            
            return Task.CompletedTask;
        }
    }
}