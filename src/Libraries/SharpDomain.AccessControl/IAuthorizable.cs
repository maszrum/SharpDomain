namespace SharpDomain.AccessControl
{
    public interface IAuthorizable
    {
        void ConfigureAuthorization(AuthorizationConfiguration configuration);
    }
}
