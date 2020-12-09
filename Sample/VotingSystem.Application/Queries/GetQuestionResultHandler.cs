using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Queries
{
    internal class GetQuestionResultHandler : IRequestHandler<GetQuestionResult, QuestionResultViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionResultsRepository _questionResultsRepository;

        public GetQuestionResultHandler(IMapper mapper, IQuestionResultsRepository questionResultsRepository)
        {
            _mapper = mapper;
            _questionResultsRepository = questionResultsRepository;
        }

        public async Task<QuestionResultViewModel> Handle(GetQuestionResult request, CancellationToken cancellationToken)
        {
            // TODO: validate request
            // user not voted? -> access denied
            
            var questionResult = await _questionResultsRepository.GetQuestionResultByQuestionId(request.QuestionId);
            
            if (questionResult is null)
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            var viewModel = _mapper.Map<QuestionResult, QuestionResultViewModel>(questionResult);
            
            return viewModel;
        }
    }
}