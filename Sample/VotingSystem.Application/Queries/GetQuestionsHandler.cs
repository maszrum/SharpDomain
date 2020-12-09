using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Queries
{
    // ReSharper disable once UnusedType.Global
    internal class GetQuestionsHandler : IRequestHandler<GetQuestions, QuestionsListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionsRepository _questionsRepository;

        public GetQuestionsHandler(
            IMapper mapper, 
            IQuestionsRepository questionsRepository)
        {
            _mapper = mapper;
            _questionsRepository = questionsRepository;
        }

        public async Task<QuestionsListViewModel> Handle(GetQuestions request, CancellationToken cancellationToken)
        {
            // TODO: validation of request
            
            var questions = await _questionsRepository.GetAll();
            
            var viewModel = _mapper.Map<IEnumerable<Question>, QuestionsListViewModel>(questions);
            
            return viewModel;
        }
    }
}