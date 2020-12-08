using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Application.Exceptions;
using SharpDomain.Application.ViewModels;
using SharpDomain.Core.InfrastructureAbstractions;
using SharpDomain.Core.Models;

namespace SharpDomain.Application.Queries
{
    // ReSharper disable once UnusedType.Global
    internal class GetMyModelHandler : IRequestHandler<GetMyModel, MyModelViewModel>
    {
        private readonly IMyModelRepository _repository;
        private readonly IMapper _mapper;

        public GetMyModelHandler(
            IMyModelRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MyModelViewModel> Handle(GetMyModel request, CancellationToken cancellationToken)
        {
            // validate request
            
            var model = await _repository.Get(request.Id);
            if (model is null)
            {
                throw new ObjectNotFoundException<MyModel>(request.Id);
            }

            var viewModel = _mapper.Map<MyModel, MyModelViewModel>(model);
            // map model to view model
            return viewModel;
        }
    }
}