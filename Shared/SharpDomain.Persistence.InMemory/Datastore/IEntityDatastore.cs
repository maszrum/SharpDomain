namespace SharpDomain.Persistence.InMemory.Datastore
{
    internal interface IEntityDatastore
    {
        void Commit();
        void Rollback();
        void SetSourceToOrigin();
        void SetSourceToCopy();
    }
}