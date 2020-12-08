using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Voter : AggregateRoot<Voter>
    {
        public Voter(Guid id)
        {
            Id = id;
        }

        public override Guid Id { get; }

        public static IDomainResult<Voter> Create()
        {
            var id = Guid.NewGuid();
            var model = new Voter(id);
            
            var createdEvent = new VoterCreated(model);
            
            return Event(createdEvent, model);
        }
    }
}