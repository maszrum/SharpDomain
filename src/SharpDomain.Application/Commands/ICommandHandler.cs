using MediatR;

namespace SharpDomain.Application
{
    public interface ICommandHandler<in TCommand, TData> : IRequestHandler<TCommand, Response<TData>> 
        where TCommand : ICommand<TData>
        where TData : class
    {
    }
}