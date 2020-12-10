using System.Collections.Generic;
using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class QuestionsListToViewModel : Profile
    {
        public QuestionsListToViewModel()
        {
            CreateMap<IEnumerable<Question>, QuestionsListViewModel>()
                .ForMember(
                    dest => dest.Questions, 
                    opt => opt.MapFrom(src => src));
        }
    }
}