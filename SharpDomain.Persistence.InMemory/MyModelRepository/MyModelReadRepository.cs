using System;
using System.Threading.Tasks;
using SharpDomain.Core.InfrastructureAbstractions;
using SharpDomain.Core.Models;
using SharpDomain.Persistence.InMemory.Datastore;

namespace SharpDomain.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelReadRepository : IMyModelRepository
    {
        private readonly IDatastore _datastore;

        public MyModelReadRepository(IDatastore datastore)
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