using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;
using VotingSystem.Core.ValueObjects;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IVotersRepository
    {
        Task<Voter?> GetVoterByPesel(Pesel pesel);
        Task<bool> VoterExists(Guid id);
        Task<int> GetVotersCount();
    }
}