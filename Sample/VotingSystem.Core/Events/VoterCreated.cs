using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class VoterCreated : INotification
    {
        public VoterCreated(Voter voter)
        {
            Voter = voter;
        }
        
        public Voter Voter { get; }
    }
}