using System;
using System.Threading.Tasks;
using MySample.Core.Models;

namespace MySample.Core.InfrastructureAbstractions
{
    public interface IMyModelRepository
    {
        Task<MyModel?> Get(Guid id);
    }
}