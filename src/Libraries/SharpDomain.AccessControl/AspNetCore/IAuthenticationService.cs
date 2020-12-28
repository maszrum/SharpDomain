﻿using SharpDomain.AccessControl;

namespace VotingSystem.AccessControl.AspNetCore
{
    public interface IAuthenticationService<TIdentity> : IIdentityService<TIdentity>
        where TIdentity : IIdentity
    {
        void SetIdentity(TIdentity identity);
        string GenerateToken(TIdentity identity);
    }
}