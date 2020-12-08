using System;
using System.Collections.Generic;
using System.Linq;
using SharpDomain.Core;
using VotingSystem.Core.Events;

namespace VotingSystem.Core.Models
{
    public class VotingResult : AggregateRoot<VotingResult>
    {
        public VotingResult(Guid id, Dictionary<Guid, int> votes)
        {
            Id = id;
            _votes = votes;
        }
        
        public override Guid Id { get; }
        
        private readonly Dictionary<Guid, int> _votes;
        public IReadOnlyDictionary<Guid, int> Votes => _votes;
        
        public IDomainResult<VotingResult> IncrementVotes(Guid answerId)
        {
            if (!_votes.ContainsKey(answerId))
            {
                // TODO: proper exception
                throw new Exception();
            }
            
            _votes[answerId]++;
            
            var @event = new VotingResultIncremented(this, answerId);
            
            return Event(@event);
        }
        
        public static IDomainResult<VotingResult> Create(IEnumerable<Guid> questionsId)
        {
            var id = Guid.NewGuid();
            var votes = questionsId.ToDictionary(questionId => questionId, _ => 0);
            
            var votingResult = new VotingResult(id, votes);
            
            var @event = new VotingResultCreated(votingResult);
            
            return Event(@event, votingResult);
        }
    }
}