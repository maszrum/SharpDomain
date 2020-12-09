using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetMyAnswers : IRequest<MyAnswersViewModel>
    {
        public Guid VoterId { get; set; }
    }
}