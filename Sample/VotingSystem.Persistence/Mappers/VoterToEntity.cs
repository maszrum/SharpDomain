using AutoMapper;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.Mappers
{
    internal class VoterToEntity : Profile
    {
        public VoterToEntity()
        {
            CreateMap<Voter, VoterEntity>();
        }
    }
}