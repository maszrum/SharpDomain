using MediatR;
using SharpDomain.Application.ViewModels;

namespace SharpDomain.Application.Commands
{
    public class CreateMyModel : IRequest<MyModelViewModel>
    {
        public CreateMyModel()
        {
        }
        
        public CreateMyModel(string? stringProperty, int intProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
        }
        
        public string? StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}