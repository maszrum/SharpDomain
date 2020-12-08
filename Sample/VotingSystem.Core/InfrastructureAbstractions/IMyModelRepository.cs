using System;
using System.Threading.Tasks;
using SampleDomain.Core.Models;

namespace SampleDomain.Core.InfrastructureAbstractions
{
    public interface IMyModelRepository
    {
        Task<MyModel?> Get(Guid id);
    }
}