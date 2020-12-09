using System;

namespace VotingSystem.Persistence.Entities
{
    public class AnswerResultEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionResultId { get; set; }
        public Guid AnswerId { get; set; }
        public int Votes { get; set; }
    }
}