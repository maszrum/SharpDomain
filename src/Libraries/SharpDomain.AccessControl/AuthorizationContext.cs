namespace SharpDomain.AccessControl
{
    public class AuthorizationContext
    {
        public bool AccessGranted { get; private set; }

        public void GrantAccess()
        {
            AccessGranted = true;
        }
    }
}
