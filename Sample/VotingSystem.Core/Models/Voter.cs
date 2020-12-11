using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.ValueObjects;

namespace VotingSystem.Core.Models
{
    public class Voter : AggregateRoot<Voter>
    {
        public Voter(
            Guid id, 
            Pesel pesel,
            bool isAdministrator)
        {
            Id = id;
            Pesel = pesel;
            IsAdministrator = isAdministrator;
        }

        public override Guid Id { get; }
        
        public Pesel Pesel { get; }
        
        public bool IsAdministrator { get; set; }
        
        public static IDomainResult<Voter> Create(string? pesel)
        {
            var id = Guid.NewGuid();
            var peselValue = new Pesel(pesel);
            
            var model = new Voter(id, peselValue, isAdministrator: false);
            
            var createdEvent = new VoterCreated(model);
            
            return Event(createdEvent, model);
        }
    }
}