using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySample.Application.ViewModels;
using MySample.Core.Shared;
using MySample.Core.Events;
using MySample.Core.Models;

namespace MySample.Application.Commands
{
    // ReSharper disable once UnusedType.Global
    internal class CreateMyModelHandler : IRequestHandler<CreateMyModel, MyModelViewModel>
    {
        private readonly IMapper _mapper;

        public CreateMyModelHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<MyModelViewModel> Handle(CreateMyModel request, CancellationToken cancellationToken)
        {
            // TODO: validation of request
            
            var model = MyModel.CreateNew(request.IntProperty, request.StringProperty);
            
            var @event = new MyModelCreated(model);
            await DomainEvents.Publish(@event, cancellationToken);
            
            var viewModel = _mapper.Map<MyModel, MyModelViewModel>(model);
            return viewModel;
        }
    }
}