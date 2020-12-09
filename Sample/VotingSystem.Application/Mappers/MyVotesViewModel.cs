using System.Collections.Generic;
using AutoMapper;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class MyVotesViewModel : Profile
    {
        public MyVotesViewModel()
        {
            CreateMap<IEnumerable<Vote>, MyVotesViewModel>();
        }
    }
}