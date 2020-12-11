using AutoMapper;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.Mappers
{
    internal class QuestionResultToEntity : Profile
    {
        public QuestionResultToEntity()
        {
            CreateMap<QuestionResult, QuestionResultEntity>();
        }
    }
}