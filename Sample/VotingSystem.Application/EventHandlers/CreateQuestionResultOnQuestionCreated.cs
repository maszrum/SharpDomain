using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.EventHandlers
{
    internal class CreateQuestionResultOnQuestionCreated : INotificationHandler<QuestionCreated>
    {
        private readonly IDomainEvents _domainEvents;

        public CreateQuestionResultOnQuestionCreated(IDomainEvents domainEvents)
        {
            _domainEvents = domainEvents;
        }

        public Task Handle(QuestionCreated notification, CancellationToken cancellationToken)
        {
            var question = notification.Question;
            QuestionResult.CreateFromQuestion(question)
                .CollectEvents(_domainEvents);
            
            return _domainEvents.PublishCollected(cancellationToken);
        }
    }
}