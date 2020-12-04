using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core.Events;
using SharpDomain.Core.InfrastructureInterfaces;
using SharpDomain.Core.Models;

namespace SharpDomain.Persistence.EventHandlers
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