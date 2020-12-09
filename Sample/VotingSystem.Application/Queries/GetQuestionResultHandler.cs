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
        private readonly IVotingResultsRepository _votingResultsRepository;

        public GetQuestionResultHandler(IMapper mapper, IVotingResultsRepository votingResultsRepository)
        {
            _mapper = mapper;
            _votingResultsRepository = votingResultsRepository;
        }

        public async Task<QuestionResultViewModel> Handle(GetQuestionResult request, CancellationToken cancellationToken)
        {
            // TODO: validate request
            // user not voted? -> access denied
            
            var questionResult = await _votingResultsRepository.GetQuestionResultByQuestionId(request.QuestionId);
            
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