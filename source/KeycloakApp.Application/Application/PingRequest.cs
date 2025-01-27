using System.Diagnostics;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;

namespace KeycloakApp.Application.Application;

public sealed record PingRequest(string Value) : IRequest<string>;

public sealed class PingRequestValidator : AbstractValidator<PingRequest>
{
    public PingRequestValidator()
    {
        RuleFor(x => x.Value)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage($"{nameof(PingRequest.Value)} cannot be null or empty");
    }
}

public class PingRequestPreProcessor : IRequestPreProcessor<PingRequest>
{
    public Task Process(PingRequest request, CancellationToken cancellationToken)
    {
        Debug.WriteLine("PreProcessor");
        return Task.CompletedTask;
    }
}

public class PingRequestPostProcessor : IRequestPostProcessor<PingRequest, string>
{
    public Task Process(PingRequest request, string response, CancellationToken cancellationToken)
    {
        Debug.WriteLine("PostProcessor");
        return Task.CompletedTask;
    }
}

public class PingRequestExceptionAction : IRequestExceptionAction<PingRequest, Exception>
{
    public Task Execute(PingRequest request, Exception exception, CancellationToken cancellationToken)
    {

        Debug.WriteLine($"Exception action {exception.GetType().Name}  {exception.Message}");
        return Task.CompletedTask;
    }
}

public class PingRequestExceptionHandler : IRequestExceptionHandler<PingRequest, string, Exception>
{
    public Task Handle(PingRequest request, Exception exception, RequestExceptionHandlerState<string> state, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"Exception handler {exception.GetType().Name}  {exception.Message}");

        state.SetHandled(exception.Message);

        return Task.CompletedTask;
    }
}

public class PingRequestHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("Test exception");

        ///Debug.WriteLine($"{request.Value}-Pong");
        ///return Task.FromResult($"{request.Value}-Pong");
    }
}
