using System.Diagnostics;
using Carter;
using KeycloakApp.Application.Application;
using MediatR;

namespace KeycloakApp.Api.Application;

internal sealed class CounterStream : ICarterModule
{
    private const string Path = "api/v1/stream";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(Path, static async (ISender sender, CancellationToken cancellationToken) =>
        {
            using var cts = new CancellationTokenSource();
            var count = 10;
            await foreach (var item in sender.CreateStream(new CounterStreamRequest(), cts.Token))
            {
                count--;
                if (count == 0)
                    await cts.CancelAsync();

                Debug.WriteLine($"stream item: {item}");
            }
            return Results.Ok();
        })
        .WithTags(Tags.Test);
    }
}
