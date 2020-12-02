using System.Threading;
using System.Threading.Tasks;
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
        public async Task<MyModelViewModel> Handle(CreateMyModel request, CancellationToken cancellationToken)
        {
            // here validation of request
            
            var model = MyModel.CreateNew();
            
            // rewrite properties from request to model
            
            var @event = new MyModelCreated(model);
            await DomainEvents.Publish(@event, cancellationToken);
            
            // map model to view model
            return new MyModelViewModel();
        }
    }
}