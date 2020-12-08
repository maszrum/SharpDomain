using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VotingSystem.Application.Exceptions;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Queries
{
    // ReSharper disable once UnusedType.Global
    internal class GetMyModelHandler : IRequestHandler<GetMyModel, SomeViewModel>
    {
        private readonly IVotesRepository _repository;
        private readonly IMapper _mapper;

        public GetMyModelHandler(
            IVotesRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SomeViewModel> Handle(GetMyModel request, CancellationToken cancellationToken)
        {
            // validate request
            
            var model = await _repository.Get(request.Id);
            if (model is null)
            {
                throw new ObjectNotFoundException<Vote>(request.Id);
            }

            var viewModel = _mapper.Map<Vote, SomeViewModel>(model);
            // map model to view model
            return viewModel;
        }
    }
}