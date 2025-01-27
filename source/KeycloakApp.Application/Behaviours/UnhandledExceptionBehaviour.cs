using System.Diagnostics;
using MediatR;

namespace KeycloakApp.Application.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse>() : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            Debug.WriteLine("Unhandled Exception {0} for Request {1} {2}", ex.Message, requestName, request);
            throw;
        }
    }
}
