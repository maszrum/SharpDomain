using AutoMapper;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Mappers
{
    internal class MyModelToViewModel : Profile
    {
        public MyModelToViewModel()
        {
            CreateMap<Vote, SomeViewModel>();
        }
    }
}