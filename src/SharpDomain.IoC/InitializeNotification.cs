using MediatR;

namespace SharpDomain.IoC
{
    public enum InitializationType
    {
        IfNeed,
        Forcefully
    }
    
    public class InitializeNotification : INotification
    {
        public InitializeNotification(InitializationType initializationType)
        {
            InitializationType = initializationType;
        }

        public InitializationType InitializationType { get; }
    }
}