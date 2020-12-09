using System;
using System.Threading.Tasks;
using AutoMapper;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory.Datastore;
using VotingSystem.Persistence.RepositoryInterfaces;

namespace VotingSystem.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class AnswersWriteRepository
    {
        private readonly InMemoryDatastore _datastore;
        private readonly IMapper _mapper;

        public AnswersWriteRepository(
            InMemoryDatastore datastore, 
            IMapper mapper)
        {
            _datastore = datastore;
            _mapper = mapper;
        }

        public Task Create(Vote model)
        {
            var entity = _mapper.Map<Vote, QuestionEntity>(model);
            
            _datastore.MyModels.Add(entity.Id, entity);
            
            return Task.CompletedTask;
        }

        public Task Update(Vote model)
        {
            var entity = _mapper.Map<Vote, QuestionEntity>(model);
            
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