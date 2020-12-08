using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Application;
using SharpDomain.Core;
using VotingSystem.Application.ViewModels;
using VotingSystem.Core.Models;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Commands
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