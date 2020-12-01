using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySample.Application.ViewModels;
using MySample.Core.Models;

namespace MySample.Application.Commands
{
    internal class IncrementMyModelValueHandler : IRequestHandler<IncrementMyModelValue, MyModelViewModel>
    {
        public Task<MyModelViewModel> Handle(IncrementMyModelValue request, CancellationToken cancellationToken)
        {
            // validate request
            
            // read model from repository
            var model = MyModel.CreateNew();
            
            model.IncrementInteger();
            
            // map model to view model
            return Task.FromResult(new MyModelViewModel());
        }
    }
}