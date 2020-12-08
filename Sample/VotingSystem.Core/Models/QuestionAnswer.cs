using System;
using SharpDomain.Core;

namespace VotingSystem.Core.Models
{
    public class QuestionAnswer : Aggregate<QuestionAnswer>
    {
        public QuestionAnswer(Guid id, int order, string answer)
        {
            Id = id;
            Order = order;
            Answer = answer;
        }
        
        public override Guid Id { get; }
        
        public int Order { get; }
        
        public string Answer { get; }

        public override string ToString() => $"{Order}. {Answer}";
    }
}