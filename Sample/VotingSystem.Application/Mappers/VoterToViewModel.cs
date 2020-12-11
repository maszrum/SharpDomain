using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class VoterToViewModel : Profile
    {
        public VoterToViewModel()
        {
            CreateMap<Voter, VoterViewModel>();
        }
    }
}