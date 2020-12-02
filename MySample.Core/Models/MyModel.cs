using System;
using System.Threading.Tasks;
using MySample.Core.Exceptions;
using MySample.Core.Shared;

namespace MySample.Core.Models
{
    public class MyModel : AggregateRoot<MyModel>
    {
        private const int MaxIntegerValue = 10;

        private MyModel(Guid id)
        {
            Id = id;
            StringProperty = string.Empty;
        }

        public string StringProperty { get; set; }
        
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

        public static MyModel CreateNew()
        {
            var id = Guid.NewGuid();
            return new MyModel(id);
        }
    }
}