using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SharpDomain.Core;
using VotingSystem.Application.ViewModels;

// ReSharper disable once UnusedType.Global

namespace VotingSystem.Application.Commands
{
    internal class SomeCommandHandler : IRequestHandler<SomeCommand, SomeViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IDomainEvents _domainEvents;

        public SomeCommandHandler(
            IMapper mapper, 
            IDomainEvents domainEvents)
        {
            _mapper = mapper;
            _domainEvents = domainEvents;
        }

        public Task<SomeViewModel> Handle(SomeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}