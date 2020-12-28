using System.Threading.Tasks;

namespace SharpDomain.AccessControl
{
    public interface IAuthorizationRequirement
    {
        Task Handle(AuthorizationContext context);
    }
}
