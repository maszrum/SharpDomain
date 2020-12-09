using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Commands
{
    internal class VoteForHandler : AsyncRequestHandler<VoteFor>
    {
        private readonly IDomainEvents _domainEvents;
        private readonly IVotesRepository _votesRepository;

        public VoteForHandler(
            IDomainEvents domainEvents, 
            IVotesRepository votesRepository)
        {
            _domainEvents = domainEvents;
            _votesRepository = votesRepository;
        }

        protected override async Task Handle(VoteFor request, CancellationToken cancellationToken)
        {
            var voterVotes = await _votesRepository.GetByVoter(request.VoterId);
            
            var alreadyVoted = voterVotes.Any(v => v.QuestionId == request.QuestionId);
            if (alreadyVoted)
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            Vote.Create(request.VoterId, request.QuestionId, request.AnswerId)
                .CollectEvents(_domainEvents);
            
            await _domainEvents.PublishCollected(cancellationToken);
        }
    }
}