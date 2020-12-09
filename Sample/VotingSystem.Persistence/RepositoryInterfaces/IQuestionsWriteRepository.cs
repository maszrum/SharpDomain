using System.Threading.Tasks;
using VotingSystem.Persistence.Entities;

namespace VotingSystem.Persistence.RepositoryInterfaces
{
    public interface IQuestionsWriteRepository
    {
        Task Create(QuestionEntity question);
    }
}