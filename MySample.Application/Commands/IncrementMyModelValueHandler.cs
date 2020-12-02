using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySample.Application.Exceptions;
using MySample.Application.ViewModels;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Application.Commands
{
    // ReSharper disable once UnusedType.Global
    internal class IncrementMyModelValueHandler : IRequestHandler<IncrementMyModelValue, MyModelViewModel>
    {
        private readonly IMyModelReadRepository _repository;
        private readonly IMapper _mapper;

        public IncrementMyModelValueHandler(
            IMyModelReadRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MyModelViewModel> Handle(IncrementMyModelValue request, CancellationToken cancellationToken)
        {
            // TODO: validate request
            
            var model = await _repository.Get(request.Id);
            if (model is null)
            {
                throw new ObjectNotFoundException<MyModel>(request.Id);
            }
            
            await model.IncrementInteger();
            
            var viewModel = _mapper.Map<MyModel, MyModelViewModel>(model);
            return viewModel;
        }
    }
}