using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Application.Exceptions;
using SharpDomain.Application.ViewModels;
using SharpDomain.Core.InfrastructureAbstractions;
using SharpDomain.Core.Models;
using SharpDomain.Core.Shared;

namespace SharpDomain.Application.Commands
{
    // ReSharper disable once UnusedType.Global
    internal class IncrementMyModelValueHandler : IRequestHandler<IncrementMyModelValue, MyModelViewModel>
    {
        private readonly IMyModelRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEvents _domainEvents;

        public IncrementMyModelValueHandler(
            IMyModelRepository repository, 
            IMapper mapper, 
            IDomainEvents domainEvents)
        {
            _repository = repository;
            _mapper = mapper;
            _domainEvents = domainEvents;
        }

        public async Task<MyModelViewModel> Handle(IncrementMyModelValue request, CancellationToken cancellationToken)
        {
            // TODO: validate request
            
            var model = await _repository.Get(request.Id);
            
            if (model is null)
            {
                throw new ObjectNotFoundException<MyModel>(request.Id);
            }
            
            model.IncrementInteger()
                .CollectEvents(_domainEvents);
            
            using (model.CollectPropertiesChange(_domainEvents))
            {
                model.StringProperty = "Changed string";
            }
            
            await _domainEvents.PublishCollected(cancellationToken);
            
            var viewModel = _mapper.Map<MyModel, MyModelViewModel>(model);
            return viewModel;
        }
    }
}