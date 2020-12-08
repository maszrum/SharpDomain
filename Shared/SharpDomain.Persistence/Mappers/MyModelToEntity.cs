using AutoMapper;
using SampleDomain.Core.Models;
using SharpDomain.Persistence.Entities;

namespace SharpDomain.Persistence.Mappers
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