using AutoMapper;
using SharpDomain.Application.ViewModels;
using SharpDomain.Core.Models;

namespace SharpDomain.Application.Mappers
{
    internal class MyModelToViewModel : Profile
    {
        public MyModelToViewModel()
        {
            CreateMap<MyModel, MyModelViewModel>();
        }
    }
}