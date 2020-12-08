using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VotingSystem.Core.Events;

namespace VotingSystem.Application.EventHandlers
{
    // ReSharper disable once UnusedType.Global
    internal class SendEmailOnMyModelCreated : INotificationHandler<VotePosted>
    {
        public Task Handle(VotePosted notification, CancellationToken cancellationToken)
        {
            // TODO: here send email
            // you can inject some dependencies in ctor
            
            return Task.CompletedTask;
        }
    }
}