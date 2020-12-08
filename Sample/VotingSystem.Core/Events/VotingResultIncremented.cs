using System;
using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class VotingResultIncremented : INotification
    {
        public VotingResultIncremented(VotingResult votingResult, Guid answerId)
        {
            VotingResult = votingResult;
            AnswerId = answerId;
        }

        public VotingResult VotingResult { get; }
        public Guid AnswerId { get; }
    }
}