using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Externals
{
    public interface IExternalService
    {
        Task<TResponse> CallExternalServiceAsync<TPayload, TResponse>(
            string endpoint, 
            TPayload payload, 
            CancellationToken cancellationToken) 
            where TPayload : class
            where TResponse : class;
    }
}
