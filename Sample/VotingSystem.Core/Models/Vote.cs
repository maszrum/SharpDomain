using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Vote : AggregateRoot<Vote>
    {
        public Vote(Guid id, Guid voterId)
        {
            Id = id;
        }

        public override Guid Id { get; }

        public static IDomainResult<Vote> Create(Guid voterId)
        {
            var id = Guid.NewGuid();
            var model = new Vote(id, voterId);
            
            var createdEvent = new VotePosted(model);
            
            return Event(createdEvent, model);
        }
    }
}