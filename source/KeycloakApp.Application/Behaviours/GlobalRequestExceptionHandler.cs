using System.Diagnostics;
using MediatR.Pipeline;

namespace KeycloakApp.Application.Behaviours;

public class GlobalRequestExceptionHandler<TRequest, TResponse, TException>
  : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : notnull
    where TException : Exception
{
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {

        Debug.WriteLine($"Something went wrong while handling request of type {typeof(TRequest).Name} {exception.Message}");

        ///state.SetHandled(default!);

        return Task.CompletedTask;
    }
}
