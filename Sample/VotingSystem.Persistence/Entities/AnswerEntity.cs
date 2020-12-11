using System;

namespace VotingSystem.Persistence.Entities
{
    public class AnswerEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public int Order { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}