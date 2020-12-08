using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Commands
{
    public class SomeCommand : IRequest<SomeViewModel>
    {
        public SomeCommand(string? stringProperty, int intProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
        }
        
        public string? StringProperty { get; }
        public int IntProperty { get; }
    }
}