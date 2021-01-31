using NUnit.Framework;
using SharpDomain.Responses;

namespace SharpDomain.NUnit
{
    // must use TError in static class because TData type cannot be inferred when
    // public static TError Of<TError, TData>(Response<TData> response)
    internal static class AssertError<TError> where TError : ErrorBase
    {
        public static TError Of<TData>(Response<TData> response) 
            where TData : class 
        {
            if (!response.IsError)
            {
                var message = $"expected error {typeof(TError).Name}, received no error";
                Assert.Fail(message);
                return default!;
            }
            
            var error = response.Error as TError;
            
            if (error is null)
            {
                var expectedErrorTypeName = typeof(TError).Name;
                var actualErrorTypeName = response.Error!.GetType().Name;
                var message = $"response is error, but not {expectedErrorTypeName}, actual: {actualErrorTypeName}";
            
                Assert.Fail(message);
            }
            
            return error!;
        }
    }
}