using Microsoft.AspNetCore.Builder;
using SharpDomain.AccessControl;

namespace VotingSystem.AccessControl.AspNetCore
{
    public static class AspNetCoreExtensions
    {
        public static IApplicationBuilder UseIdentity<TIdentity>(this IApplicationBuilder app) 
            where TIdentity : IIdentity
        {
            return app.UseMiddleware<AuthenticationMiddleware<TIdentity>>();
        }
    }
}