using System;
using System.Threading.Tasks;
using SampleDomain.Core.InfrastructureAbstractions;
using SampleDomain.Core.Models;
using SampleDomain.Persistence.InMemory.Datastore;

namespace SampleDomain.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelReadRepository : IMyModelRepository
    {
        private readonly InMemoryDatastore _datastore;

        public MyModelReadRepository(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }

        public Task<MyModel?> Get(Guid id)
        {
            if (!_datastore.MyModels.TryGetValue(id, out var entity))
            {
                return Task.FromResult<MyModel?>(null);
            }

            var model = new MyModel(entity.Id, entity.IntProperty, entity.StringProperty);
            
            return Task.FromResult(model)!;
        }
    }
}