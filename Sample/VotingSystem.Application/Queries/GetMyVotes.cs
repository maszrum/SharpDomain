using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetMyAnswers : IRequest<MyVotesViewModel>
    {
        public Guid VoterId { get; set; }
    }
}