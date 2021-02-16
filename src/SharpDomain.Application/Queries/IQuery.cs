using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface IQuery<TData> : IRequest<Response<TData>>
        where TData : notnull
    {
    }
}