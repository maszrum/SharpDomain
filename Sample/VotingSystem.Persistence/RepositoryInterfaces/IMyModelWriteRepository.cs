using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IMyModelWriteRepository
    {
        Task Create(Vote model);
        Task Update(Vote model);
        Task Delete(Guid id);
    }
}