using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using SharpDomain.AccessControl;

namespace VotingSystem.AccessControl.AspNetCore
{
    public interface IClaimsIdentityConverter
    {
        bool TryGetIdentity<TIdentity>(IEnumerable<Claim> claims, [NotNullWhen(true)] out TIdentity identity) 
            where TIdentity : IIdentity;

        IEnumerable<Claim> GetClaims<TIdentity>(TIdentity identity) 
            where TIdentity : IIdentity;
    }
}