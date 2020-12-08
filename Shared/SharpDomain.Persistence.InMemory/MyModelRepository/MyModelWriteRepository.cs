using System;
using System.Threading.Tasks;
using AutoMapper;
using SampleDomain.Core.Models;
using SampleDomain.Persistence.Entities;
using SampleDomain.Persistence.RepositoryInterfaces;
using SharpDomain.Persistence.InMemory.Datastore;

namespace SharpDomain.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelWriteRepository : IMyModelWriteRepository
    {
        private readonly InMemoryDatastore _datastore;
        private readonly IMapper _mapper;

        public MyModelWriteRepository(
            InMemoryDatastore datastore, 
            IMapper mapper)
        {
            _datastore = datastore;
            _mapper = mapper;
        }

        public Task Create(MyModel model)
        {
            var entity = _mapper.Map<MyModel, MyModelEntity>(model);
            
            _datastore.MyModels.Add(entity.Id, entity);
            
            return Task.CompletedTask;
        }

        public Task Update(MyModel model)
        {
            var entity = _mapper.Map<MyModel, MyModelEntity>(model);
            
            _datastore.MyModels[entity.Id] = entity;
            
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            _datastore.MyModels.Remove(id);
            
            return Task.CompletedTask;
        }
    }
}