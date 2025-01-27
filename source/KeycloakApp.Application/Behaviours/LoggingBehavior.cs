using MediatR;

namespace KeycloakApp.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Pre-processing
        Console.WriteLine($"Handling {typeof(TRequest).Name}");

        var response = await next();

        // Post-processing
        Console.WriteLine($"Handled {typeof(TResponse).Name}");

        return response;
    }
}


