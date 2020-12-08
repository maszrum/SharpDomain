using AutoMapper;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.Mappers
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelToEntity : Profile
    {
        public MyModelToEntity()
        {
            CreateMap<MyModel, MyModelEntity>();
        }
    }
}