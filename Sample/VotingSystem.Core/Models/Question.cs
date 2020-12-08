using System;
using System.Collections.Generic;
using System.Linq;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Question : AggregateRoot<Question>
    {
        public Question(Guid id, string questionText, IEnumerable<QuestionAnswer> answers)
        {
            Id = id;
            QuestionText = questionText;
            _answers = answers as List<QuestionAnswer> ?? answers.ToList();
        }
        
        public override Guid Id { get; }
        
        public string QuestionText { get; }
        
        private readonly List<QuestionAnswer> _answers;
        public IReadOnlyList<QuestionAnswer> Answers => _answers;
        
        public static IDomainResult<Question> Create(string questionText, IEnumerable<QuestionAnswer> answers)
        {
            var id = Guid.NewGuid();
            var question = new Question(id, questionText, answers);
            
            var @event = new QuestionCreated(question);
            
            return Event(@event, question);
        }
    }
}