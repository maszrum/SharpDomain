using AutoMapper;
using SampleDomain.Core.Models;
using SampleDomain.Persistence.Entities;

namespace SampleDomain.Persistence.Mappers
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