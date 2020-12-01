using System;
using System.Threading.Tasks;
using MySample.Core.Models;

namespace MySample.Application.InfrastructureInterfaces
{
    internal interface IMyModelWriteRepository
    {
        Task<MyModel> Create(MyModel model);
        Task Update(MyModel model);
        Task Delete(Guid id);
    }
}