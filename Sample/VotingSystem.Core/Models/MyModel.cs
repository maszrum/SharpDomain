using System;
using SharpDomain.Core;
using VotingSystem.Core.Events;
using VotingSystem.Core.Exceptions;

namespace VotingSystem.Core.Models
{
    public class MyModel : AggregateRoot<MyModel>
    {
        private const int MaxIntegerValue = 10;

        public MyModel(Guid id, int intProperty, string? stringProperty)
        {
            Id = id;
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }

        public string? StringProperty { get; set; }
        
        public int IntProperty { get; private set; }

        public IDomainResult<MyModel> IncrementInteger()
        {
            if (IntProperty >= MaxIntegerValue)
            {
                throw new MaximumValueReachedException();
            }
            
            IntProperty++;
            
            var propertyChangedEvent = new ModelChanged<MyModel>(this, nameof(IntProperty));
            
            return Event(propertyChangedEvent);
        }

        public static IDomainResult<MyModel> Create(int intProperty, string? stringProperty)
        {
            var id = Guid.NewGuid();
            var model = new MyModel(id, intProperty, stringProperty);
            
            var createdEvent = new MyModelCreated(model);
            
            return Event(createdEvent, model);
        }
    }
}