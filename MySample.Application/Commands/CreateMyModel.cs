using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Commands
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