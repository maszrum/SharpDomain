using System;
using System.Threading.Tasks;
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

        public async Task IncrementInteger()
        {
            if (IntProperty >= MaxIntegerValue)
            {
                throw new MaximumValueReachedException();
            }
            
            await using(this.PublishChanges())
            {
                IntProperty++;
            }
        }

        public static MyModel CreateNew(int intProperty, string? stringProperty)
        {
            var id = Guid.NewGuid();
            return new MyModel(id, intProperty, stringProperty);
        }
    }
}