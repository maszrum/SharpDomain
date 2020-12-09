using System;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Core.InfrastructureAbstractions;
using VotingSystem.Core.Models;
using VotingSystem.Core.ValueObjects;
using VotingSystem.Persistence.Entities;
using VotingSystem.Persistence.InMemory.Datastore;
using VotingSystem.Persistence.RepositoryInterfaces;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Persistence.InMemory.Repositories
{
    internal class VotersRepository : IVotersRepository, IVotersWriteRepository
    {
        private readonly InMemoryDatastore _datastore;

        public VotersRepository(InMemoryDatastore datastore)
        {
            _datastore = datastore;
        }

        public Task<Voter?> GetVoterByPesel(Pesel pesel)
        {
            var voterEntity = _datastore.Voters.Values
                .SingleOrDefault(e => pesel.Equals(e.Pesel));
            
            if (voterEntity == default)
            {
                return Task.FromResult(default(Voter));
            }
            
            var peselObject = new Pesel(voterEntity.Pesel);
            var voter = new Voter(voterEntity.Id, peselObject, voterEntity.IsAdministrator);
            
            return Task.FromResult((Voter?)voter);
        }

        public Task<bool> VoterExists(Guid id)
        {
            var exists = _datastore.Voters.ContainsKey(id);
            return Task.FromResult(exists);
        }

        public Task<int> GetVotersCount()
        {
            var count = _datastore.Voters.Count;
            return Task.FromResult(count);
        }

        public Task Create(VoterEntity voter)
        {
            if (_datastore.Voters.ContainsKey(voter.Id))
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            _datastore.Voters.Add(voter.Id, voter);
            
            return Task.CompletedTask;
        }

        public Task Update(VoterEntity voter)
        {
            if (!_datastore.Voters.ContainsKey(voter.Id))
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            _datastore.Voters[voter.Id] = voter;
            
            return Task.CompletedTask;
        }
    }
}