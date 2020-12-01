using System;
using System.Threading.Tasks;
using MySample.Core.Models;

namespace MySample.Application.InfrastructureInterfaces
{
    internal interface IMyModelReadRepository
    {
        Task<MyModel> Get(Guid id);
    }
}