using System;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory.Datastore;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.InMemory.Repositories
{
    internal class QuestionResultsRepository : IQuestionResultsRepository, IQuestionResultsWriteRepository
    {
        private readonly InMemoryDatastore _datastore;

        public QuestionResultsRepository(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }

        public Task<QuestionResult?> GetQuestionResultByQuestionId(Guid questionId)
        {
            var questionResultEntity = _datastore.QuestionResults.Values
                .SingleOrDefault(e => e.QuestionId == questionId);
            
            if (questionResultEntity == default)
            {
                return Task.FromResult(default(QuestionResult));
            }
            
            var answerResultEntities = _datastore.AnswerResults.Values
                .Where(e => e.QuestionResultId == questionResultEntity.Id);
            
            var answerResults = answerResultEntities
                .Select(e => new AnswerResult(e.Id, e.QuestionResultId, e.AnswerId, e.Votes))
                .ToArray();
            
            var questionResult = new QuestionResult(
                questionResultEntity.Id, 
                questionResultEntity.QuestionId,
                answerResults);
            
            return Task.FromResult((QuestionResult?)questionResult);
        }

        public Task Create(QuestionResultEntity questionResult)
        {
            if (_datastore.QuestionResults.ContainsKey(questionResult.Id))
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            _datastore.QuestionResults.Add(questionResult.Id, questionResult);
            
            return Task.CompletedTask;
        }
    }
}