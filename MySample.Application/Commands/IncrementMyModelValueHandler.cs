using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySample.Application.Exceptions;
using MySample.Application.ViewModels;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Application.Commands
{
    internal class IncrementMyModelValueHandler : IRequestHandler<IncrementMyModelValue, MyModelViewModel>
    {
        private readonly IMyModelReadRepository _repository;

        public IncrementMyModelValueHandler(IMyModelReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<MyModelViewModel> Handle(IncrementMyModelValue request, CancellationToken cancellationToken)
        {
            // validate request
            
            var model = await _repository.Get(request.Id);
            if (model is null)
            {
                throw new ObjectNotFoundException<MyModel>(request.Id);
            }
            
            await model.IncrementInteger();
            
            // map model to view model
            return new MyModelViewModel();
        }
    }
}