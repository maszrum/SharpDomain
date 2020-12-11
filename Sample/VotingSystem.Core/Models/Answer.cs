using System;
using SharpDomain.Core;

namespace VotingSystem.Core.Models
{
    public class Answer : Aggregate<Answer>
    {
        public Answer(
            Guid id, 
            Guid questionId,
            int order,
            string text)
        {
            Id = id;
            QuestionId = questionId;
            Order = order;
            Text = text;
        }
        
        public override Guid Id { get; }
        
        public Guid QuestionId { get; }
        
        public int Order { get; }
        
        public string Text { get; }

        public override string ToString() => $"{Order}. {Text}";
    }
}