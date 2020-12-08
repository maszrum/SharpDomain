using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class VotePosted : INotification
    {
        public VotePosted(Vote vote)
        {
            Vote = vote;
        }

        public Vote Vote { get; }
    }
}