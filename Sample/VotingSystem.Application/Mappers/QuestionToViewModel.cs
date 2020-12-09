using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Mappers
{
    internal class QuestionToViewModel : Profile
    {
        public QuestionToViewModel()
        {
            CreateMap<Question, QuestionViewModel>();
        }
    }
}