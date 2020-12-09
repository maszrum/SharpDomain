using System;

namespace VotingSystem.Persistence.Entities
{
    public class VoteEntity
    {
        public Guid Id { get; set; }
        public Guid VoterId { get; set; }
        public Guid QuestionId { get; set; }
    }
}