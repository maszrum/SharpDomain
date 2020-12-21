using MediatR;

namespace SharpDomain.Application
{
    public interface IQueryHandler<in TQuery, TData> : IRequestHandler<TQuery, Response<TData>> 
        where TQuery : IRequest<Response<TData>>
        where TData : class
    {
    }
}