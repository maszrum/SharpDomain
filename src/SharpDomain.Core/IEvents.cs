namespace SharpDomain.Core
{
    public interface IEvents
    {
        void Add(params EventBase[] events);
    }
}