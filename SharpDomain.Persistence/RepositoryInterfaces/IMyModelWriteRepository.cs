using System;
using System.Threading.Tasks;
using SharpDomain.Core.Models;

namespace SharpDomain.Core.InfrastructureInterfaces
{
    public interface IMyModelWriteRepository
    {
        Task Create(MyModel model);
        Task Update(MyModel model);
        Task Delete(Guid id);
    }
}