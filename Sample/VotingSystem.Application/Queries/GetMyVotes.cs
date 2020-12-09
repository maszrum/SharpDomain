using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetMyVotes : IRequest<MyVotesViewModel>
    {
        public Guid VoterId { get; set; }
    }
}