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
    internal class VoterCreatedHandler : INotificationHandler<VoterCreated>
    {
        private readonly IMapper _mapper;
        private readonly IVotersWriteRepository _votersWriteRepository;

        public VoterCreatedHandler(
            IMapper mapper, 
            IVotersWriteRepository votersWriteRepository)
        {
            _mapper = mapper;
            _votersWriteRepository = votersWriteRepository;
        }

        public Task Handle(VoterCreated notification, CancellationToken cancellationToken)
        {
            var voter = notification.Voter;
            var voterEntity = _mapper.Map<Voter, VoterEntity>(voter);
            
            return _votersWriteRepository.Create(voterEntity);
        }
    }
}