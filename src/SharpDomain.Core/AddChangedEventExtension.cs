namespace SharpDomain.Core
{
    public static class AddChangedEventExtension
    {
        public static void AddChangedEvent<TModel>(
            this IEvents events, params string[] changedPropertyNames) where TModel : Aggregate
        {
            var @event = new ModelChanged<TModel>(changedPropertyNames);
            events.Add(@event);
        }
    }
}