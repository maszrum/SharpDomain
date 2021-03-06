﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharpDomain.Core;
using SharpDomain.Responses;

namespace SharpDomain.IoC.Core
{
    public class DomainExceptionBehavior<TRequest, TData> : IPipelineBehavior<TRequest, Response<TData>> 
        where TRequest : notnull
        where TData : notnull
    {
        public async Task<Response<TData>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Response<TData>> next)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (DomainException exception)
            {
                return new DomainError(exception);
            }
        }
    }
}