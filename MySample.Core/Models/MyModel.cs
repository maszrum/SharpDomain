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
        }

        private string _stringProperty;
        public string StringProperty
        {
            get => _stringProperty;
            set => SetPropertyValue(model => model.StringProperty, ref _stringProperty, value);
        }

        private int _intProperty;
        public int IntProperty
        {
            get => _intProperty;
            private set => SetPropertyValue(model => model.IntProperty, ref _intProperty, value);
        }

        public Task IncrementInteger()
        {
            if (IntProperty >= MaxIntegerValue) throw new MaximumValueReachedException();

            return ModifyProperties(model => model.IntProperty++);
        }

        public static MyModel CreateNew()
        {
            var id = Guid.NewGuid();
            return new MyModel(id);
        }
    }
}