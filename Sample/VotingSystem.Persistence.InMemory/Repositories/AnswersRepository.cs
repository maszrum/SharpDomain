using System;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory.Datastore;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.InMemory.Repositories
{
    internal class AnswersRepository : IAnswersWriteRepository
    {
        private readonly InMemoryDatastore _datastore;

        public AnswersRepository(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }

        public Task Create(params AnswerEntity[] answers)
        {
            var anyExist = answers.Any(a => _datastore.Answers.ContainsKey(a.Id));
            if (anyExist)
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            foreach (var answer in answers)
            {
                _datastore.Answers.Add(answer.Id, answer);
            }
            
            return Task.CompletedTask;
        }
    }
}