namespace SharpDomain.AccessControl
{
    public interface IAuthorizationRequired
    {
        void ConfigureAuthorization(AuthorizationConfiguration configuration);
    }
}
