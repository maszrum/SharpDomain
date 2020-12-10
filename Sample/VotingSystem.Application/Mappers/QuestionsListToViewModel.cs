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
                    source => source.Questions, 
                    opt => opt.MapFrom(questions => questions));
        }
    }
}