using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Commands
{
    internal class CreateQuestionHandler : IRequestHandler<CreateQuestion, QuestionViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IDomainEvents _domainEvents;

        public CreateQuestionHandler(
            IMapper mapper, 
            IDomainEvents domainEvents)
        {
            _mapper = mapper;
            _domainEvents = domainEvents;
        }

        public async Task<QuestionViewModel> Handle(CreateQuestion request, CancellationToken cancellationToken)
        {
            // TODO: validate request
            
            var model = Question.Create(request.QuestionText, request.Answers)
                .CollectEvents(_domainEvents);
            
            await _domainEvents.PublishCollected(cancellationToken);
            
            var viewModel = _mapper.Map<Question, QuestionViewModel>(model);
            return viewModel;
        }
    }
}