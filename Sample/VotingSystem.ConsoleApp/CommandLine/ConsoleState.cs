using System;

namespace VotingSystem.ConsoleApp.CommandLine
{
    internal class ConsoleState
    {
        public Guid VoterId { get; set; }
        public string? VoterPesel { get; set; }
    }
}