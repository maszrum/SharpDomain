using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IMyModelRepository
    {
        Task<MyModel?> Get(Guid id);
    }
}