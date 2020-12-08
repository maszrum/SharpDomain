using System;
using MediatR;
using SampleDomain.Application.ViewModels;

namespace SampleDomain.Application.Commands
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