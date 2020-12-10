using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetMyVotes : IRequest<MyVotesViewModel>
    {
        public GetMyVotes(Guid voterId)
        {
            VoterId = voterId;
        }

        public Guid VoterId { get; }
    }
}