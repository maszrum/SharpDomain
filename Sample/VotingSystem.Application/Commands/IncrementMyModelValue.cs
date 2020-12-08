using System;
using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Commands
{
    public class IncrementMyModelValue : IRequest<MyModelViewModel>
    {
        public IncrementMyModelValue(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}