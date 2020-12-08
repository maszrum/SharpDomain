using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Commands
{
    public class CreateMyModel : IRequest<MyModelViewModel>
    {
        public CreateMyModel(string? stringProperty, int intProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
        }
        
        public string? StringProperty { get; }
        public int IntProperty { get; }
    }
}