using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetVotingResult : IRequest<VotingResultViewModel>
    {
        public GetVotingResult(Guid questionId, Guid voterId)
        {
            QuestionId = questionId;
            VoterId = voterId;
        }

        public Guid QuestionId { get; }
        public Guid VoterId { get; }
    }
}