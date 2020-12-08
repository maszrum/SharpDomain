using MediatR;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.Events
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