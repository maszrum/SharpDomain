using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface  IVotesWriteRepository
    {
        Task Create(VoteEntity vote);
    }
}