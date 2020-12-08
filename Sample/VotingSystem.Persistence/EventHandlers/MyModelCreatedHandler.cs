using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VotingSystem.Core.Events;
using VotingSystem.Persistence.RepositoryInterfaces;

namespace VotingSystem.Persistence.EventHandlers
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelCreatedHandler : INotificationHandler<VotePosted>
    {
        private readonly IMyModelWriteRepository _repository;

        public MyModelCreatedHandler(IMyModelWriteRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(VotePosted notification, CancellationToken cancellationToken) => 
            _repository.Create(notification.Vote);
    }
}