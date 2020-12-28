using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface ICommand : IRequest<Response<Empty>>
    {
    }
    
    public interface ICommand<TData> : IRequest<Response<TData>>
        where TData : class
    {
    }
}