using System;
using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Commands
{
    public class IncrementMyModelValue : IRequest<MyModelViewModel>
    {
        public Guid Id { get; set; }
    }
}