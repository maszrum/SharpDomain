using MediatR;
using SharpDomain.Core.Models;

namespace SharpDomain.Core.Events
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