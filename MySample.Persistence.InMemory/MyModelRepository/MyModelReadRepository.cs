using System;
using System.Threading.Tasks;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelReadRepository : IMyModelReadRepository
    {
        public Task<MyModel?> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}