using System;
using System.Threading.Tasks;
using MySample.Core.InfrastructureInterfaces;
using MySample.Core.Models;

namespace MySample.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    internal class MyModelWriteRepository : IMyModelWriteRepository
    {
        public Task Create(MyModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(MyModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}