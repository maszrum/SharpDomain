using System;

namespace SharpDomain.AccessControl
{
    public interface IIdentity
    {
        Guid Id { get; }
        bool IsValid();
    }
}