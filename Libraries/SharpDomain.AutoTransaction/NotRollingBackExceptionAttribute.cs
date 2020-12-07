using System;

namespace SharpDomain.AutoTransaction
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotRollingBackAttribute : Attribute
    {
    }
}