using MediatR;
using VotingSystem.Application.ViewModels;

namespace VotingSystem.Application.Queries
{
    public class LogIn : IRequest<VoterViewModel>
    {
        public LogIn(string? pesel)
        {
            Pesel = pesel;
        }

        public string? Pesel { get; }
    }
}