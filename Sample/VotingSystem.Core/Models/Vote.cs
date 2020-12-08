using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Vote : AggregateRoot<Vote>
    {
        public Vote(Guid id, Guid voterId, Guid questionAnswerId)
        {
            Id = id;
            VoterId = voterId;
            QuestionAnswerId = questionAnswerId;
        }

        public override Guid Id { get; }
        
        public Guid VoterId { get; }

        public Guid QuestionAnswerId { get; }
        
        public static IDomainResult<Vote> Create(Guid voterId, Guid questionAnswerId)
        {
            var id = Guid.NewGuid();
            var model = new Vote(id, voterId, questionAnswerId);
            
            var createdEvent = new VotePosted(model);
            
            return Event(createdEvent, model);
        }
    }
}