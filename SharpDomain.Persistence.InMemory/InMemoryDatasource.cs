using System;
using System.Collections.Concurrent;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.InMemory
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class InMemoryDatasource
    {
        public ConcurrentDictionary<Guid, MyModelEntity> MyModels { get; } =
            new ConcurrentDictionary<Guid, MyModelEntity>();
    }
}