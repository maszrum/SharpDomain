using System;

namespace VotingSystem.Persistence.Entities
{
    public class QuestionResultEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
    }
}