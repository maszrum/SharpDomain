using System;
using MediatR;
using SampleDomain.Application.ViewModels;

namespace SampleDomain.Application.Queries
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