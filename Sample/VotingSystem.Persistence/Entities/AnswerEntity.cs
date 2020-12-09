using System;

namespace VotingSystem.Persistence.Entities
{
    public class AnswerEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
    }
}