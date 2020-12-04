using System;
using MediatR;
using SharpDomain.Application.ViewModels;

namespace SharpDomain.Application.Queries
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