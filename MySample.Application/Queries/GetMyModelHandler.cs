using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySample.Application.Exceptions;
using MySample.Application.ViewModels;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Application.Queries
{
    internal class GetMyModelHandler : IRequestHandler<GetMyModel, MyModelViewModel>
    {
        private readonly IMyModelReadRepository _repository;
        private readonly IMapper _mapper;

        public GetMyModelHandler(
            IMyModelReadRepository repository, 
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