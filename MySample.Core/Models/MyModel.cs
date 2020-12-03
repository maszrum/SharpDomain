using System;
using MySample.Core.Events;
using MySample.Core.Exceptions;
using MySample.Core.Shared;

namespace MySample.Core.Models
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