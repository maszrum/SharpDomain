using System;
using System.Threading.Tasks;
using MySample.Core.Events;
using MySample.Core.Exceptions;

namespace MySample.Core.Models
{
    public class MyModel : AggregateRoot
    {
        private const int MaxIntegerValue = 10;
        
        private MyModel(Guid id)
        {
            Id = id;
        }

        public string StringProperty { get; set; }
        public int IntProperty { get; private set; }
        
        public async Task IncrementInteger()
        {
            if (IntProperty >= MaxIntegerValue)
            {
                throw new MaximumValueReachedException();
            }
            
            IntProperty++;
            
            var @event = new MyModelChanged(this);
            await DomainEvents.Publish(@event);
        }
        
        public static MyModel CreateNew()
        {
            var id = Guid.NewGuid(); 
            return new MyModel(id);
        }
    }
}