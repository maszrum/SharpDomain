using System;
using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface ICreateCommand : IRequest<Response<Guid>>
    {
    }
}