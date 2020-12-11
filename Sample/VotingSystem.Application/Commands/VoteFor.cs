using System;
using MediatR;

namespace VotingSystem.Application.Commands
{
    public class VoteFor : IRequest
    {
        public VoteFor(Guid voterId, Guid questionId, Guid answerId)
        {
            VoterId = voterId;
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public Guid VoterId { get; }
        public Guid QuestionId { get; }
        public Guid AnswerId { get; }
    }
}