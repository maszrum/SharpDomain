﻿using NUnit.Framework;
using SharpDomain.Responses;

namespace SharpDomain.NUnit
{
    public static class AssertNotError
    {
        public static TData Of<TData>(Response<TData> response) where TData : class
        {
            if (response.IsError)
            {
                var message = $"expected not error, received error {response.Error!.GetType().Name}";
                Assert.Fail(message);
                return default!;
            }
            
            return response.Data!;
        }
    }
}