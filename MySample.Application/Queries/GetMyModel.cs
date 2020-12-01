using System;
using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Queries
{
    public class GetMyModel : IRequest<MyModelViewModel>
    {
        public Guid Id { get; set; }
    }
}