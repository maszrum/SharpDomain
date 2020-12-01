using MediatR;
using MySample.Core.Models;

namespace MySample.Core.Events
{
    public class MyModelCreated : INotification
    {
        public MyModelCreated(MyModel model)
        {
            Model = model;
        }

        public MyModel Model { get; }
    }
}