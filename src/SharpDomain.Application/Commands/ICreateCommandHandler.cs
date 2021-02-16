using System;
using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface ICreateCommandHandler<in TCommand> : IRequestHandler<TCommand, Response<Guid>>
        where TCommand : ICreateCommand
    {
    }
}