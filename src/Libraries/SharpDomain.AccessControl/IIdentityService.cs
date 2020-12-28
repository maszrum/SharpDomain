namespace SharpDomain.AccessControl
{
    public interface IIdentityService<out TIdentity> 
        where TIdentity : IIdentity
    {
        bool IsSignedIn { get; }
        TIdentity GetIdentity();
    }
}