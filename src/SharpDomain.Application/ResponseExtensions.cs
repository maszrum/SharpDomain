using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SharpDomain.Errors;

namespace SharpDomain.Application
{
    public static class ResponseExtensions
    {
        public static Task<TData> OnError<TData>(
            this Task<Response<TData>> responseTask,
            Action<ErrorBase> onError) 
            where TData : class
        {
            return responseTask.ContinueWith(
                task => task.Result.OnError(onError));
        }
        
        public static TData OnError<TData>(
            this Response<TData> response, 
            Action<ErrorBase> onError)
            where TData : class
        {
            if (response.Data is not null)
            {
                return response.Data;
            }
            
            if (response.Error is not null)
            {
                onError(response.Error);
            }
            
            throw new NullReferenceException($"{nameof(response.Data)} is null");
        }
        
        public static bool TryGet<TData>(this Response<TData> response, [NotNullWhen(true)] out TData? data) 
            where TData : class
        {
            data = response.Data;
            return data is not null;
        }
    }
}