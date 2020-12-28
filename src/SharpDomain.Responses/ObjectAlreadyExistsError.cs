using System.Reflection;

namespace SharpDomain.Responses
{
    public class ObjectAlreadyExistsError : ErrorBase
    {
        private ObjectAlreadyExistsError(MemberInfo objectType)
        {
            Message = $"object of type {objectType.Name} already exists";
        }

        public override string Message { get; }
        
        public static ObjectAlreadyExistsError CreateFor<TObject>() => 
            new ObjectAlreadyExistsError(typeof(TObject));
    }
}