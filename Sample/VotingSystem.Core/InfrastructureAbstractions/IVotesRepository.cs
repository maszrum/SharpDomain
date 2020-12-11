using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Core.InfrastructureAbstractions
{
    public interface IVotesRepository
    {
        Task<IReadOnlyList<Vote>> GetByVoter(Guid voterId);
    }
}