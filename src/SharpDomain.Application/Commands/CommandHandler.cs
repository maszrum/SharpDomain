using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Responses;

namespace SharpDomain.Application
{
    public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Response<Empty>> 
        where TCommand : ICommand
    {
        public abstract Task<Response<Empty>> Handle(TCommand request, CancellationToken cancellationToken);
        
        protected Empty Nothing() => new();
    }
}