using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.InMemory.Datastore
{
    // TODO: internal
    public interface IDatastore
    {
        IDictionary<Guid, MyModelEntity> MyModels { get;}
        Task<Transaction> BeginTransaction();
    }
}