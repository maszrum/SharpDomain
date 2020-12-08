using System;
using MediatR;
using SharpDomain.Application.ViewModels;

namespace SharpDomain.Application.Commands
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