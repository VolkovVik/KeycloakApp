using System.Diagnostics;
using Carter;
using KeycloakApp.Application.Application;
using KeycloakApp.Application.Notification;
using MediatR;

namespace KeycloakApp.Api.Application;

internal sealed class Ping : ICarterModule
{
    private const string Path = "api/v1/ping";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(Path, static async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new PingRequest(nameof(PingRequest));
            var result = await mediator.Send(command, cancellationToken);

            await mediator.Publish(new PingNotification(), cancellationToken);
            Debug.WriteLine("Ping");

            return Results.Ok(result);
        })
        .WithTags(Tags.Test);
    }
}
