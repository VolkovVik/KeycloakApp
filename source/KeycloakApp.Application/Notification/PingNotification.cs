using System.Diagnostics;
using MediatR;

namespace KeycloakApp.Application.Notification;

public sealed class PingNotification : INotification { }

public sealed class Pong1 : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Pong 1");
        return Task.CompletedTask;
    }
}

public sealed class Pong2 : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Pong 2");
        return Task.CompletedTask;
    }
}
