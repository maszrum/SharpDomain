using System;
using System.Collections.Generic;
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
    internal class QuestionsRepository : IQuestionsRepository, IQuestionsWriteRepository
    {
        private readonly InMemoryDatastore _datastore;

        public QuestionsRepository(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }

        public Task<IReadOnlyList<Question>> GetAll()
        {
            Dictionary<Guid, List<AnswerEntity>> answerEntitiesGrouped = _datastore.Answers.Values
                .GroupBy(e => e.QuestionId)
                .ToDictionary(g => g.Key, g => g.ToList());
            
            IEnumerable<AnswerEntity> GetAnswersInQuestion(Guid questionId) =>
                answerEntitiesGrouped.TryGetValue(questionId, out var result) 
                    ? result 
                    : Array.Empty<AnswerEntity>();

            var questionEntities = _datastore.Questions.Values;
            
            var questions = questionEntities
                .Select(qe =>
                {
                    var answerEntities = GetAnswersInQuestion(qe.Id);
                    var answers = answerEntities.Select(ae => new Answer(ae.Id, ae.QuestionId, ae.Order, ae.Text));
                    
                    return new Question(qe.Id, qe.QuestionText, answers);
                })
                .ToList();
            
            return Task.FromResult((IReadOnlyList<Question>)questions);
        }

        public Task Create(QuestionEntity question)
        {
            if (_datastore.Questions.ContainsKey(question.Id))
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            _datastore.Questions.Add(question.Id, question);
            
            return Task.CompletedTask;
        }
    }
}