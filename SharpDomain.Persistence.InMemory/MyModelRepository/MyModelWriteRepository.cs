using System;
using System.Threading.Tasks;
using AutoMapper;
using SharpDomain.Core.InfrastructureInterfaces;
using SharpDomain.Core.Models;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelWriteRepository : IMyModelWriteRepository
    {
        private readonly InMemoryDatasource _datasource;
        private readonly IMapper _mapper;

        public MyModelWriteRepository(
            InMemoryDatasource datasource, 
            IMapper mapper)
        {
            _datasource = datasource;
            _mapper = mapper;
        }

        public Task Create(MyModel model)
        {
            var entity = _mapper.Map<MyModel, MyModelEntity>(model);
            
            if (!_datasource.MyModels.TryAdd(entity.Id, entity))
            {
                throw new Exception(
                    $"cannot add object with specified id: {entity.Id}");
            }
            
            return Task.CompletedTask;
        }

        public Task Update(MyModel model)
        {
            var entity = _mapper.Map<MyModel, MyModelEntity>(model);
            
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