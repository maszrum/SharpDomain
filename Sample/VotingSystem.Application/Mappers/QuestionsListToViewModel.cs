using System.Collections.Generic;
using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Mappers
{
    internal class QuestionsListToViewModel : Profile
    {
        public QuestionsListToViewModel()
        {
            CreateMap<IEnumerable<Question>, QuestionsListViewModel>();
        }
    }
}