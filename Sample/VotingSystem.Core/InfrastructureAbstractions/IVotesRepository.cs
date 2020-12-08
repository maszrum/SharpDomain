using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IVotesRepository
    {
        Task<Vote?> Get(Guid id);
    }
}