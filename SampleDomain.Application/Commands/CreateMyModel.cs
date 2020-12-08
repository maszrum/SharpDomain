using MediatR;
using SampleDomain.Application.ViewModels;

namespace SampleDomain.Application.Commands
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