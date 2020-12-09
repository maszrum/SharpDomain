using System;

namespace VotingSystem.Persistence.Entities
{
    public class VoterEntity
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; } = string.Empty;
    }
}