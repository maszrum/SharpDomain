using System;
using System.Threading.Tasks;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;
using VotingSystem.Persistence.InMemory.Datastore;

namespace VotingSystem.Persistence.InMemory.MyModelRepository
{
    // ReSharper disable once UnusedType.Global
    // internal class VotesReadRepository : IVotesRepository
    // {
    //     private readonly InMemoryDatastore _datastore;
    //
    //     public VotesReadRepository(InMemoryDatastore datastore)
    //     {
    //         _datastore = datastore;
    //     }
    //
    //     public Task<Vote?> Get(Guid id)
    //     {
    //         if (!_datastore.MyModels.TryGetValue(id, out var entity))
    //         {
    //             return Task.FromResult<Vote?>(null);
    //         }
    //
    //         var model = new Vote(entity.Id, entity.IntProperty, entity.StringProperty);
    //         
    //         return Task.FromResult(model)!;
    //     }
    // }
}