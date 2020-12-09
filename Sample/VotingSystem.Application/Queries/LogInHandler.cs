using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;
using VotingSystem.Core.ValueObjects;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Queries
{
    internal class LogInHandler : IRequestHandler<LogIn, VoterViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IVotersRepository _votersRepository;

        public LogInHandler(IMapper mapper, IVotersRepository votersRepository)
        {
            _mapper = mapper;
            _votersRepository = votersRepository;
        }

        public async Task<VoterViewModel> Handle(LogIn request, CancellationToken cancellationToken)
        {
            var pesel = Pesel.ValidateAndCreate(request.Pesel);
            
            var voter = await _votersRepository.GetVoterByPesel(pesel);
            
            if (voter is null)
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            var viewModel = _mapper.Map<Voter, VoterViewModel>(voter);
            return viewModel;
        }
    }
}