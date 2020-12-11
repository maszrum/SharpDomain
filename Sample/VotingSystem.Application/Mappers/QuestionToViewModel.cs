using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Mappers
{
    internal class QuestionToViewModel : Profile
    {
        public QuestionToViewModel()
        {
            CreateMap<Answer, QuestionViewModel.AnswerViewModel>();
            CreateMap<Question, QuestionViewModel>();
        }
    }
}