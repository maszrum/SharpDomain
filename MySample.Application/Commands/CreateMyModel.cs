using MediatR;
using MySample.Application.ViewModels;

namespace MySample.Application.Commands
{
    public class CreateMyModel : IRequest<MyModelViewModel>
    {
        public string? StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}