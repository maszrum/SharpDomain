using System;
using System.Reflection;

namespace SharpDomain.Responses
{
    public class ObjectNotFoundError : ErrorBase
    {
        private ObjectNotFoundError(MemberInfo objectType, Guid objectId)
        {
            Message = $"object of type {objectType.Name} with id {objectId} was not found";
        }

        public override string Message { get; }
        
        public static ErrorBase CreateFor<TObject>(Guid objectId) => 
            new ObjectNotFoundError(typeof(TObject), objectId);
    }
}