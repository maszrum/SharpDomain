using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InMemoryDatastore : IDatastore
    {
        private readonly ModelDatastore<MyModelEntity> _myModelStore 
            = new ModelDatastore<MyModelEntity>();
        
        public IDictionary<Guid, MyModelEntity> MyModels => _myModelStore.Models;
        
        public Task<Transaction> BeginTransaction() =>
            Task.FromResult(new Transaction(this));

        public Task Commit()
        {
            _myModelStore.Commit();
            
            return Task.CompletedTask;
        }
        
        public Task Rollback()
        {
            _myModelStore.Rollback();
            
            return Task.CompletedTask;
        }
    }
}