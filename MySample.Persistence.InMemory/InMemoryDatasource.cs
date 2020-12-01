using System;
using System.Collections.Concurrent;
using MySample.Persistence.Entities;

namespace MySample.Persistence.InMemory
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class InMemoryDatasource
    {
        public ConcurrentDictionary<Guid, MyModelEntity> MyModels { get; } =
            new ConcurrentDictionary<Guid, MyModelEntity>();
    }
}