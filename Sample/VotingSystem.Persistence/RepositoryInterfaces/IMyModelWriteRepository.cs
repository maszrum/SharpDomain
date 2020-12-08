using System;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IMyModelWriteRepository
    {
        Task Create(MyModel model);
        Task Update(MyModel model);
        Task Delete(Guid id);
    }
}