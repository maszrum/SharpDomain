using AutoMapper;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.Mappers
{
    internal class QuestionToEntity : Profile
    {
        public QuestionToEntity()
        {
            CreateMap<Question, QuestionEntity>();
        }
    }
}