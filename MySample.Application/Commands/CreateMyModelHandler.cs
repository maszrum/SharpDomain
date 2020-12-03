using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySample.Application.ViewModels;
using MySample.Core.Models;
using MySample.Core.Shared;

namespace MySample.Application.Commands
{
    // ReSharper disable once UnusedType.Global
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