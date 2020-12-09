using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class VoterChangedHandler : INotificationHandler<ModelChanged<Voter>>
    {
        public Task Handle(ModelChanged<Voter> notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}