using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Application.Shared;
using SharpDomain.Application.ViewModels;
using SharpDomain.Core.Models;
using SharpDomain.Core.Shared;

// ReSharper disable once UnusedType.Global

namespace SharpDomain.Application.Commands
{
    internal class CreateMyModelHandler : IRequestHandler<CreateMyModel, MyModelViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IDomainEvents _domainEvents;

        public CreateMyModelHandler(
            IMapper mapper, 
            IDomainEvents domainEvents)
        {
            _mapper = mapper;
            _domainEvents = domainEvents;
        }

        public async Task<MyModelViewModel> Handle(CreateMyModel request, CancellationToken cancellationToken)
        {
            // TODO: validation of request
            
            var model = MyModel.Create(request.IntProperty, request.StringProperty)
                .CollectEvents(_domainEvents);
            
            await _domainEvents.PublishCollected(cancellationToken);
            
            var viewModel = _mapper.Map<MyModel, MyModelViewModel>(model);
            return viewModel;
        }
    }
}