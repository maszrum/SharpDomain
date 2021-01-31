using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SharpDomain.AccessControl.AspNetCore
{
    public static class AspNetCoreExtensions
    {
        public static void AddAuthenticationMiddleware<TIdentity>(this IServiceCollection services) 
            where TIdentity : IIdentity
        {
            services.AddScoped<AuthenticationMiddleware<TIdentity>>();
        }

        public static IApplicationBuilder UseIdentity<TIdentity>(this IApplicationBuilder app) 
            where TIdentity : IIdentity
        {
            return app.UseMiddleware<AuthenticationMiddleware<TIdentity>>();
        }
    }
}