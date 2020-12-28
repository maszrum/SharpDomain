using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SharpDomain.AccessControl;

namespace VotingSystem.AccessControl.AspNetCore
{
    public class AuthenticationMiddleware<TIdentity> : IMiddleware
        where TIdentity : IIdentity
    {
        private readonly IClaimsIdentityConverter _claimsIdentityConverter;
        private readonly IAuthenticationService<TIdentity> _authenticationService;

        public AuthenticationMiddleware(
            IClaimsIdentityConverter claimsProvider, 
            IAuthenticationService<TIdentity> authenticationService)
        {
            _claimsIdentityConverter = claimsProvider;
            _authenticationService = authenticationService;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claims = context.User.Claims;
            
            if (_claimsIdentityConverter.TryGetIdentity<TIdentity>(claims, out var identity))
            {
                _authenticationService.SetIdentity(identity);
            }
            
            return next(context);
        }
    }
}