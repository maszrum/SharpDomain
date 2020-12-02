using AutoMapper;
using MySample.Application.ViewModels;
using MySample.Core.Models;

namespace MySample.Application.Mappers
{
    internal class MyModelToViewModel : Profile
    {
        public MyModelToViewModel()
        {
            CreateMap<MyModel, MyModelViewModel>();
        }
    }
}