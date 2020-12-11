using System.Linq;
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
    internal class QuestionCreatedHandler : INotificationHandler<QuestionCreated>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionsWriteRepository _questionsWriteRepository;
        private readonly IAnswersWriteRepository _answersWriteRepository;

        public QuestionCreatedHandler(
            IMapper mapper,
            IQuestionsWriteRepository questionsWriteRepository, 
            IAnswersWriteRepository answersWriteRepository)
        {
            _mapper = mapper;
            _questionsWriteRepository = questionsWriteRepository;
            _answersWriteRepository = answersWriteRepository;
        }

        public async Task Handle(QuestionCreated notification, CancellationToken cancellationToken)
        {
            // add question
            var question = notification.Question;
            var questionEntity = _mapper.Map<Question, QuestionEntity>(question);
            
            await _questionsWriteRepository.Create(questionEntity);
            
            // add answers
            var answers = question.Answers;
            var answerEntities = answers
                .Select(a => _mapper.Map<Answer, AnswerEntity>(a))
                .ToArray();
            
            await _answersWriteRepository.Create(answerEntities);
        }
    }
}