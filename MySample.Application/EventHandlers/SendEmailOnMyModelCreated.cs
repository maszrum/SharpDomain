using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySample.Core.Events;

namespace MySample.Application.EventHandlers
{
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