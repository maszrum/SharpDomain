using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core.Events;

namespace SharpDomain.Application.EventHandlers
{
    // ReSharper disable once UnusedType.Global
    internal class SendEmailOnMyModelCreated : INotificationHandler<MyModelCreated>
    {
        public Task Handle(MyModelCreated notification, CancellationToken cancellationToken)
        {
            // TODO: here send email
            // you can inject some dependencies in ctor
            
            return Task.CompletedTask;
        }
    }
}