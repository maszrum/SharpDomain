using System;

namespace SharpDomain.Transactions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisableAutoTransactionsAttribute : Attribute
    {
    }
}