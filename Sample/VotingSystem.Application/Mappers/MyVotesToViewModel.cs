using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class MyVotesToViewModel : Profile
    {
        public MyVotesToViewModel()
        {
            CreateMap<IEnumerable<Vote>, MyVotesViewModel>()
                .ForMember(
                    dest => dest.QuestionsId,
                    opt => opt.MapFrom(src => src.Select(v => v.QuestionId)));
        }
    }
}