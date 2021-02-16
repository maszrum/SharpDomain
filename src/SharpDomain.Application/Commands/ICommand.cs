using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface ICommand : IRequest<Response<Empty>>
    {
    }
}