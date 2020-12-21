namespace SharpDomain.Errors
{
    public abstract class ErrorBase
    {
        public abstract string Message { get; }

        public override string ToString() => $"{GetType().Name}: {Message}";
    }
}