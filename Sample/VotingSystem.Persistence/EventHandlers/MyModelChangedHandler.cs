using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.RepositoryInterfaces;

namespace VotingSystem.Persistence.EventHandlers
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelChangedHandler : INotificationHandler<ModelChanged<MyModel>>
    {
        private readonly IMyModelWriteRepository _repository;

        public MyModelChangedHandler(IMyModelWriteRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(ModelChanged<MyModel> notification, CancellationToken cancellationToken) => 
            _repository.Update(notification.Model);
    }
}