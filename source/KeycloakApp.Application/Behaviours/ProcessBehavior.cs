using System.Diagnostics;
using MediatR;
using MediatR.Pipeline;

namespace KeycloakApp.Application.Behaviours;

public class ProcessBehavior<TRequest, TResponse>(
    IEnumerable<IRequestPreProcessor<TRequest>> PreProcessors,
    IEnumerable<IRequestPostProcessor<TRequest, TResponse>> PostProcessors,
    IEnumerable<IRequestExceptionAction<TRequest, Exception>> ExceptionActions,
    IEnumerable<IRequestExceptionHandler<TRequest, TResponse, Exception>> ExceptionHandlers)
    : IPipelineBehavior<TRequest, TResponse>
       where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var count1 = ExceptionHandlers.Count();
        var count2 = ExceptionActions.Count();
        Debug.WriteLine($"count1 {count1} count2 {count2}");

        // Pre-processing
        foreach (var preProcessor in PreProcessors)
            await preProcessor.Process(request, cancellationToken);

        var response = await next();

        // Post-processing
        foreach (var postProcessor in PostProcessors)
            await postProcessor.Process(request, response, cancellationToken);

        return response;
    }
}


