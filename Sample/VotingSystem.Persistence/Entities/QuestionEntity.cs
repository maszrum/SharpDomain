using System;

namespace VotingSystem.Persistence.Entities
{
    public class QuestionEntity
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
    }
}