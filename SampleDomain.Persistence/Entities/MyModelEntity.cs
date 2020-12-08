using System;

namespace SampleDomain.Persistence.Entities
{
    public class MyModelEntity
    {
        public Guid Id { get; set; }
        public string? StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}