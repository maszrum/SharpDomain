using System;

namespace VotingSystem.Application.Exceptions
{
    internal class ObjectNotFoundException<T> : ApplicationException
    {
        public Type ObjectType { get; }
        public Guid Id { get; }
        
        public ObjectNotFoundException(Guid id) 
            : base($"object with id {id} of type {typeof(T).Name} was not found")
        {
            ObjectType = typeof(T);
            Id = id;
        }
    }
}