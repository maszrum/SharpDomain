using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class AnswerResultChangedHandler : INotificationHandler<ModelChanged<AnswerResult>>
    {
        public Task Handle(ModelChanged<AnswerResult> notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}