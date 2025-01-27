using FluentValidation;
using KeycloakApp.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakApp.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = AssemblyReference.Assembly!;

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(config =>
        {
            config.AutoRegisterRequestProcessors = true;
            config.RegisterServicesFromAssemblies(assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ProcessBehavior<,>));
            ///config.AddBehavior(typeof(IRequestExceptionHandler<,,>), typeof(GlobalRequestExceptionHandler<,,>));
        });

        return services;
    }
}
