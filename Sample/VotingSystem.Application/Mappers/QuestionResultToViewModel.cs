using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class QuestionResultToViewModel : Profile
    {
        public QuestionResultToViewModel()
        {
            CreateMap<AnswerResult, QuestionResultViewModel.AnswerResultViewModel>();
            CreateMap<QuestionResult, QuestionResultViewModel>();
        }
    }
}