using MediatR;
using MySample.Core.Models;

namespace MySample.Core.Events
{
    public class MyModelChanged : INotification
    {
        public MyModelChanged(MyModel model)
        {
            Model = model;
        }

        public MyModel Model { get; }
    }
}