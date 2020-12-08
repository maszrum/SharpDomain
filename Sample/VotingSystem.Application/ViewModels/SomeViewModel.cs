using System;
using System.Text;

namespace VotingSystem.Application.ViewModels
{
    public class SomeViewModel
    {
        public Guid Id { get; set; }

        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"# {nameof(SomeViewModel)}")
                .AppendLine($"{nameof(Id)}: {Id}")
                .ToString();
    }
}