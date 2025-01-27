using System.Runtime.CompilerServices;
using FluentValidation;
using MediatR;

namespace KeycloakApp.Application.Application;

public sealed record CounterStreamRequest : IStreamRequest<int>;

public class CounterStreamValidator : AbstractValidator<CounterStreamRequest>;

public sealed class CounterStreamHandler : IStreamRequestHandler<CounterStreamRequest, int>
{
    public async IAsyncEnumerable<int> Handle(CounterStreamRequest request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var counter = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500, cancellationToken);
            yield return counter;
            counter++;
        }
    }
}
