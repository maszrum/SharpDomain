using System;
using System.Collections.Generic;
using System.Linq;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Question : AggregateRoot<Question>
    {
        public Question(
            Guid id, 
            string questionText, 
            IEnumerable<Answer> answers)
        {
            Id = id;
            QuestionText = questionText;
            _answers = answers as List<Answer> ?? answers.ToList();
        }
        
        public override Guid Id { get; }
        
        public string QuestionText { get; }
        
        private readonly List<Answer> _answers;
        public IReadOnlyList<Answer> Answers => _answers;
        
        public static IDomainResult<Question> Create(string questionText, IEnumerable<string> answers)
        {
            var questionId = Guid.NewGuid();
            
            var answerModels = answers.Select(
                    (answerText, index) =>
                    {
                        var answerId = Guid.NewGuid();
                        var order = index + 1;
                        var questionAnswer = new Answer(answerId, questionId, order, answerText);
                        return questionAnswer;
                    })
                .ToList();
            
            if (answerModels.Count < 2)
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            var question = new Question(questionId, questionText, answerModels);
            
            var @event = new QuestionCreated(question);
            
            return Event(@event, question);
        }
    }
}