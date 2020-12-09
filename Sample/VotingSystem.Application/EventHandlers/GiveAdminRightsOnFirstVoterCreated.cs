using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.InfrastructureAbstractions;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.EventHandlers
{
    internal class GiveAdminRightsOnFirstVoterCreated : INotificationHandler<VoterCreated>
    {
        private readonly IDomainEvents _domainEvents;
        private readonly IVotersRepository _votersRepository;

        public GiveAdminRightsOnFirstVoterCreated(IDomainEvents domainEvents, IVotersRepository votersRepository)
        {
            _domainEvents = domainEvents;
            _votersRepository = votersRepository;
        }

        public async Task Handle(VoterCreated notification, CancellationToken cancellationToken)
        {
            var votersCount = await _votersRepository.GetVotersCount();
            if (votersCount == 1)
            {
                var voter = notification.Voter;
                
                using (voter.CollectPropertiesChange(_domainEvents))
                {
                    voter.IsAdministrator = true;
                }
                
                await _domainEvents.PublishCollected(cancellationToken);
            }
        }
    }
}