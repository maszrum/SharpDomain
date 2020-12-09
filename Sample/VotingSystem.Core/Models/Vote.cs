using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class Vote : AggregateRoot<Vote>
    {
        public Vote(
            Guid id, 
            Guid voterId, 
            Guid questionId)
        {
            Id = id;
            VoterId = voterId;
            QuestionId = questionId;
        }

        public override Guid Id { get; }
        
        public Guid VoterId { get; }

        public Guid QuestionId { get; }
        
        public static IDomainResult<Vote> Create(Guid voterId, Guid questionId, Guid answerId)
        {
            var id = Guid.NewGuid();
            var model = new Vote(id, voterId, questionId);
            
            var createdEvent = new VotePosted(model, answerId);
            
            return Event(createdEvent, model);
        }
    }
}