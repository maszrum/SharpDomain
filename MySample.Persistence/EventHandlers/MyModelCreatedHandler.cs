using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySample.Core.Events;
using MySample.Core.InfrastructureInterfaces;

namespace MySample.Persistence.EventHandlers
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelCreatedHandler : INotificationHandler<MyModelCreated>
    {
        private readonly IMyModelWriteRepository _repository;

        public MyModelCreatedHandler(IMyModelWriteRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(MyModelCreated notification, CancellationToken cancellationToken) => 
            _repository.Create(notification.Model);
    }
}