using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
{
    public class VotingResultCreated : INotification
    {
        public VotingResultCreated(VotingResult votingResult)
        {
            VotingResult = votingResult;
        }

        public VotingResult VotingResult { get; }
    }
}