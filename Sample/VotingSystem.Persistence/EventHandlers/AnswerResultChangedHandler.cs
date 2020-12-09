using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.EventHandlers
{
    internal class AnswerResultChangedHandler : INotificationHandler<ModelChanged<AnswerResult>>
    {
        private static readonly string[] ValidPropertiesChange = { nameof(AnswerResult.Votes) };
        
        private readonly IMapper _mapper;
        private readonly IAnswerResultsWriteRepository _answerResultsWriteRepository;

        public AnswerResultChangedHandler(
            IMapper mapper, 
            IAnswerResultsWriteRepository answerResultsWriteRepository)
        {
            _mapper = mapper;
            _answerResultsWriteRepository = answerResultsWriteRepository;
        }

        public Task Handle(ModelChanged<AnswerResult> notification, CancellationToken cancellationToken)
        {
            var invalidPropertyChanged = notification.PropertiesChanged
                .FirstOrDefault(p => !ValidPropertiesChange.Contains(p));
            
            if (!string.IsNullOrEmpty(invalidPropertyChanged))
            {
                throw new InvalidOperationException(
                    $"invalid property changed in {nameof(AnswerResult)}: {invalidPropertyChanged}");
            }
            
            var answerResult = notification.Model;
            var answerResultEntity = _mapper.Map<AnswerResult, AnswerResultEntity>(answerResult);
            
            return _answerResultsWriteRepository.Update(answerResultEntity);
        }
    }
}