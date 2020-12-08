using System;
using System.Threading.Tasks;
using SharpDomain.Core.Models;

namespace SharpDomain.Core.InfrastructureAbstractions
{
    public interface IMyModelRepository
    {
        Task<MyModel?> Get(Guid id);
    }
}