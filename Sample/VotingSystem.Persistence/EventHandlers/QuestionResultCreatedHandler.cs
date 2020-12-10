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
        private readonly IAnswerResultsWriteRepository _answerResultsWriteRepository;

        public QuestionResultCreatedHandler(
            IMapper mapper, 
            IQuestionResultsWriteRepository questionResultsWriteRepository, 
            IAnswerResultsWriteRepository answerResultsWriteRepository)
        {
            _mapper = mapper;
            _questionResultsWriteRepository = questionResultsWriteRepository;
            _answerResultsWriteRepository = answerResultsWriteRepository;
        }

        public async Task Handle(QuestionResultCreated notification, CancellationToken cancellationToken)
        {
            var questionResult = notification.QuestionResult;
            var questionResultEntity = _mapper.Map<QuestionResult, QuestionResultEntity>(questionResult);
            
            await  _questionResultsWriteRepository.Create(questionResultEntity);

            foreach (var answerResult in questionResult.AnswerResults)
            {
                var answerResultEntity = _mapper.Map<AnswerResult, AnswerResultEntity>(answerResult);
                
                await _answerResultsWriteRepository.Create(answerResultEntity);
            }
        }
    }
}