using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Core.Events;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class QuestionResultCreatedHandler : INotificationHandler<QuestionResultCreated>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionResultsWriteRepository _questionResultsWriteRepository;

        public QuestionResultCreatedHandler(
            IMapper mapper, 
            IQuestionResultsWriteRepository questionResultsWriteRepository)
        {
            _mapper = mapper;
            _questionResultsWriteRepository = questionResultsWriteRepository;
        }

        public Task Handle(QuestionResultCreated notification, CancellationToken cancellationToken)
        {
            var questionResult = notification.QuestionResult;
            var questionResultEntity = _mapper.Map<QuestionResult, QuestionResultEntity>(questionResult);
            
            return _questionResultsWriteRepository.Create(questionResultEntity);
        }
    }
}