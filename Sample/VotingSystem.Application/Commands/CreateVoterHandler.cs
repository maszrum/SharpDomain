using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Commands
{
    internal class CreateVoterHandler : IRequestHandler<CreateVoter, VoterViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IVotersRepository _voters;
        private readonly IDomainEvents _domainEvents;

        public CreateVoterHandler(
            IMapper mapper, 
            IVotersRepository voters, 
            IDomainEvents domainEvents)
        {
            _mapper = mapper;
            _voters = voters;
            _domainEvents = domainEvents;
        }

        public async Task<VoterViewModel> Handle(CreateVoter request, CancellationToken cancellationToken)
        {
            // TODO: check if already exists
            
            var voter = Voter.Create(request.Pesel)
                .CollectEvents(_domainEvents);
            
            await _domainEvents.PublishCollected(cancellationToken);
            
            var viewModel = _mapper.Map<Voter, VoterViewModel>(voter);
            return viewModel;
        }
    }
}