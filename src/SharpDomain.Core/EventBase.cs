using System.Threading;
using MediatR;

namespace SharpDomain.Core
{
    public abstract class EventBase : INotification
    {
        private static int _orderCounter;
        
        private int Order { get; }

        protected EventBase()
        {
            Order = Interlocked.Increment(ref _orderCounter);
        }

        public override int GetHashCode() => Order;
    }
}