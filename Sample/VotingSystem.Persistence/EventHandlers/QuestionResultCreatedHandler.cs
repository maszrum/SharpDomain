using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VotingSystem.Core.Events;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class QuestionResultCreatedHandler : INotificationHandler<QuestionCreated>
    {
        public Task Handle(QuestionCreated notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}