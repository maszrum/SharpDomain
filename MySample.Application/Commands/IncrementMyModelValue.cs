using System;
using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Commands
{
    public class IncrementMyModelValue : IRequest<MyModelViewModel>
    {
        public IncrementMyModelValue()
        {
        }
        
        public IncrementMyModelValue(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}