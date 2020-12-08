using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class GetMyModel : IRequest<SomeViewModel>
    {
        public GetMyModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}