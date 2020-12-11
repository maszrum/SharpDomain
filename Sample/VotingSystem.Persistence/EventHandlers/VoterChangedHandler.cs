using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class VoterChangedHandler : INotificationHandler<ModelChanged<Voter>>
    {
        private static readonly string[] ValidPropertiesChange = { nameof(Voter.IsAdministrator) };
        
        private readonly IMapper _mapper;
        private readonly IVotersWriteRepository _votersWriteRepository;

        public VoterChangedHandler(
            IMapper mapper, 
            IVotersWriteRepository votersWriteRepository)
        {
            _mapper = mapper;
            _votersWriteRepository = votersWriteRepository;
        }

        public Task Handle(ModelChanged<Voter> notification, CancellationToken cancellationToken)
        {
            var invalidPropertyChanged = notification.PropertiesChanged
                .FirstOrDefault(p => !ValidPropertiesChange.Contains(p));
            
            if (!string.IsNullOrEmpty(invalidPropertyChanged))
            {
                throw new InvalidOperationException(
                    $"invalid property changed in {nameof(Voter)}: {invalidPropertyChanged}");
            }
            
            var voter = notification.Model;
            var voterEntity = _mapper.Map<Voter, VoterEntity>(voter);
            
            return _votersWriteRepository.Update(voterEntity);
        }
    }
}