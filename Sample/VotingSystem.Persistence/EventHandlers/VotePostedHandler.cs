using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Core.Events;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class VotePostedHandler : INotificationHandler<VotePosted>
    {
        private readonly IMapper _mapper;
        private readonly IVotesWriteRepository _votesWriteRepository;

        public VotePostedHandler(IMapper mapper, IVotesWriteRepository votesWriteRepository)
        {
            _mapper = mapper;
            _votesWriteRepository = votesWriteRepository;
        }

        public Task Handle(VotePosted notification, CancellationToken cancellationToken)
        {
            var vote = notification.Vote;
            var voteEntity = _mapper.Map<Vote, VoteEntity>(vote);
            
            return _votesWriteRepository.Create(voteEntity);
        }
    }
}