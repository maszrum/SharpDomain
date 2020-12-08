using MediatR;
using SampleDomain.Core.Models;

namespace SampleDomain.Core.Events
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