using System;
using System.Threading.Tasks;
using MySample.Core.Models;

namespace MySample.Core.InfrastructureInterfaces
{
    public interface IMyModelReadRepository
    {
        Task<MyModel> Get(Guid id);
    }
}