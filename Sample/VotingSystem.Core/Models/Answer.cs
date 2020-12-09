using System;
using SharpDomain.Core;

namespace VotingSystem.Core.Models
{
    public class Answer : Aggregate<Answer>
    {
        public Answer(
            Guid id, 
            string text, 
            int order)
        {
            Id = id;
            Text = text;
            Order = order;
        }
        
        public override Guid Id { get; }
        
        public int Order { get; }
        
        public string Text { get; }

        public override string ToString() => $"{Order}. {Text}";
    }
}