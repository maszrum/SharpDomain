using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.InfrastructureAbstractions;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Core.EventHandlers
{
    internal class UpdateVotingResultOnVotePosted : INotificationHandler<VotePosted>
    {
        private readonly IDomainEvents _domainEvents;
        private readonly IAnswerResultsRepository _answerResultsRepository;

        public UpdateVotingResultOnVotePosted(
            IDomainEvents domainEvents, 
            IAnswerResultsRepository answerResultsRepository)
        {
            _domainEvents = domainEvents;
            _answerResultsRepository = answerResultsRepository;
        }

        public async Task Handle(VotePosted notification, CancellationToken cancellationToken)
        {
            var answerResult = await _answerResultsRepository.GetAnswerResultByAnswerId(notification.AnswerId);
            
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