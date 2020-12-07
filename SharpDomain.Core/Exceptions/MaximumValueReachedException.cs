using SharpDomain.Transactions;

namespace SharpDomain.Core.Exceptions
{
    [RollingBackException]
    public class MaximumValueReachedException : DomainException
    {
    }
}