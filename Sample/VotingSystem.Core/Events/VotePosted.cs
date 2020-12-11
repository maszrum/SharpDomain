using System;
using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class VotePosted : INotification
    {
        public VotePosted(Vote vote, Guid answerId)
        {
            Vote = vote;
            AnswerId = answerId;
        }

        public Vote Vote { get; }
        
        public Guid AnswerId { get; }
    }
}