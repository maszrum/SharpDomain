using System;
using System.Threading.Tasks;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;
using MySample.Persistence.Entities;

namespace MySample.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelWriteRepository : IMyModelWriteRepository
    {
        private readonly InMemoryDatasource _datasource;

        public MyModelWriteRepository(InMemoryDatasource datasource)
        {
            _datasource = datasource;
        }

        public Task Create(MyModel model)
        {
            // TODO: map model to entity
            var entity = new MyModelEntity();
            
            if (!_datasource.MyModels.TryAdd(entity.Id, entity))
            {
                throw new Exception(
                    $"cannot add object with specified id: {entity.Id}");
            }
            
            return Task.CompletedTask;
        }

        public Task Update(MyModel model)
        {
            // TODO: map model to entity
            var entity = new MyModelEntity();
            
            if (!_datasource.MyModels.TryRemove(entity.Id, out _))
            {
                throw new Exception(
                    $"cannot update object with specified id: {entity.Id}");
            }
            
            _datasource.MyModels.TryAdd(entity.Id, entity);
            
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            if (!_datasource.MyModels.TryRemove(id, out _))
            {
                throw new Exception(
                    $"cannot remove object with specified id: {id}");
            }
            
            return Task.CompletedTask;
        }
    }
}