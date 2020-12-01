using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySample.Application.ViewModels;
using MySample.Core.Models;

namespace MySample.Application.Queries
{
    internal class GetMyModelHandler : IRequestHandler<GetMyModel, MyModelViewModel>
    {
        public Task<MyModelViewModel> Handle(GetMyModel request, CancellationToken cancellationToken)
        {
            // validate request
            
            // read model from repository
            var model = MyModel.CreateNew();
            
            // map model to view model
            return Task.FromResult(new MyModelViewModel());
        }
    }
}