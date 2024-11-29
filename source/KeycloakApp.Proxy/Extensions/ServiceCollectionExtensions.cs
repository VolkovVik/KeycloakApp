using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace KeycloakApp.Proxy.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddRateLimiting(
        this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddConcurrencyLimiter("concurrency", options =>
            {
                options.PermitLimit = 10;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 5;
            });
        });

        return services;
    }
}
