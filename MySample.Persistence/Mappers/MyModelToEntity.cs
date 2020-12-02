using AutoMapper;
using MySample.Core.Models;
using MySample.Persistence.Entities;

namespace MySample.Persistence.Mappers
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