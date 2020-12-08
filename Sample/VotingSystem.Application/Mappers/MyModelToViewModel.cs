using AutoMapper;
using SampleDomain.Application.ViewModels;
using SampleDomain.Core.Models;

namespace SampleDomain.Application.Mappers
{
    internal class MyModelToViewModel : Profile
    {
        public MyModelToViewModel()
        {
            CreateMap<MyModel, MyModelViewModel>();
        }
    }
}