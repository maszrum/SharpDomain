using System;
using SharpDomain.Errors;

namespace SharpDomain.Application
{
    // thanks to @somekind from 4p
    // https://4programmers.net/Forum/C_i_.NET/345421-zwracanie_odpowiedzi_serwisu?p=1727950#id1727950

    public class Response<TData> where TData : class
    {
        public ErrorBase? Error { get; }
        public TData? Data { get; }
        public bool IsError => Error is not null;

        public Response(ErrorBase error)
        {
            Error = error;
        }

        public Response(TData data)
        {
            Data = data;
        }
        
        public T Match<T>(Func<ErrorBase, T> leftFunc, Func<TData, T> rightFunc) => 
            IsError 
                ? leftFunc(Error!) 
                : rightFunc(Data!);
        
        public static implicit operator Response<TData>(ErrorBase error) => new(error);
        
        public static implicit operator Response<TData>(TData data) => new(data);
        
        public static implicit operator TData?(Response<TData> response) => response.Data;
    }
}