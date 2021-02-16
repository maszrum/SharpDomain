using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public interface IQueryHandler<in TQuery, TData> : IRequestHandler<TQuery, Response<TData>> 
        where TQuery : IRequest<Response<TData>>
        where TData : notnull
    {
    }
}