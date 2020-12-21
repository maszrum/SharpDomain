using MediatR;

namespace SharpDomain.Application
{
    public interface IQuery<TData> : IRequest<Response<TData>>
        where TData : class
    {
    }
}