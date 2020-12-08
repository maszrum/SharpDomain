using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleDomain.Core.Events;
using SampleDomain.Persistence.RepositoryInterfaces;

namespace SampleDomain.Persistence.EventHandlers
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