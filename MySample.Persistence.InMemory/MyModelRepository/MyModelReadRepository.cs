using System;
using System.Threading.Tasks;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelReadRepository : IMyModelReadRepository
    {
        private readonly InMemoryDatasource _datasource;

        public MyModelReadRepository(InMemoryDatasource datasource)
        {
            _datasource = datasource;
        }

        public Task<MyModel?> Get(Guid id)
        {
            if (!_datasource.MyModels.TryGetValue(id, out var entity)) return Task.FromResult<MyModel?>(null);

            // TODO: map entity to model
            var model = MyModel.CreateNew();
            
            return Task.FromResult(model)!;
        }
    }
}