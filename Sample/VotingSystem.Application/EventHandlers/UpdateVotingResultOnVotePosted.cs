using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.InfrastructureAbstractions;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.EventHandlers
{
    internal class UpdateVotingResultOnVotePosted : INotificationHandler<VotePosted>
    {
        private readonly IDomainEvents _domainEvents;
        private readonly IVotingResultsRepository _votingResultsRepository;

        public UpdateVotingResultOnVotePosted(
            IDomainEvents domainEvents, 
            IVotingResultsRepository votingResultsRepository)
        {
            _domainEvents = domainEvents;
            _votingResultsRepository = votingResultsRepository;
        }

        public async Task Handle(VotePosted notification, CancellationToken cancellationToken)
        {
            var answerResult = await _votingResultsRepository.GetAnswerResultByAnswerId(notification.AnswerId);
            
            if (answerResult is null)
            {
                // TODO: proper exception
                throw new Exception("not found");
            }
            
            answerResult.IncrementVotes()
                .CollectEvents(_domainEvents);
            
            await _domainEvents.PublishCollected(cancellationToken);
        }
    }
}