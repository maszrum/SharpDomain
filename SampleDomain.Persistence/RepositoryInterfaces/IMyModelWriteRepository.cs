using System;
using System.Threading.Tasks;
using SampleDomain.Core.Models;

namespace SampleDomain.Persistence.RepositoryInterfaces
{
    public interface IMyModelWriteRepository
    {
        Task Create(MyModel model);
        Task Update(MyModel model);
        Task Delete(Guid id);
    }
}