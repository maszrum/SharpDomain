using System;
using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Queries
{
    public class GetMyModel : IRequest<MyModelViewModel>
    {
        public GetMyModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}