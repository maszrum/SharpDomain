using System;
using System.Threading.Tasks;
using SharpDomain.Core.InfrastructureAbstractions;
using SharpDomain.Core.Models;

namespace SharpDomain.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelReadRepository : IMyModelRepository
    {
        private readonly InMemoryDatasource _datasource;

        public MyModelReadRepository(InMemoryDatasource datasource)
        {
            _datasource = datasource;
        }

        public Task<MyModel?> Get(Guid id)
        {
            if (!_datasource.MyModels.TryGetValue(id, out var entity))
            {
                return Task.FromResult<MyModel?>(null);
            }

            var model = new MyModel(entity.Id, entity.IntProperty, entity.StringProperty);
            
            return Task.FromResult(model)!;
        }
    }
}