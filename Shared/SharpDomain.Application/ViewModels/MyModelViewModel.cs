using System;
using System.Text;

namespace SharpDomain.Application.ViewModels
{
    public class MyModelViewModel
    {
        public Guid Id { get; set; }
        public string? StringProperty { get; set; }
        public int IntProperty { get; set; }

        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"# {nameof(MyModelViewModel)}")
                .AppendLine($"{nameof(Id)}: {Id}")
                .AppendLine($"{nameof(StringProperty)}: {StringProperty}")
                .Append($"{nameof(IntProperty)}: {IntProperty}")
                .ToString();
    }
}