using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Commands
{
    public class CreateVoter : IRequest<VoterViewModel>
    {
        public CreateVoter(string? pesel)
        {
            Pesel = pesel;
        }

        public string? Pesel { get; }
    }
}