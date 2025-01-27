using System.Reflection;
using Carter;

namespace KeycloakApp.Api.Extensions;

internal static class CarterExtensions
{
    internal static IServiceCollection AddCarterConfiguration(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var catalog = new DependencyContextAssemblyCatalogCustom(assemblies);

#pragma warning disable S125 // Sections of code should not be commented out
        //services.AddCarter(catalog);
#pragma warning restore S125 // Sections of code should not be commented out

        var types = catalog.GetAssemblies().SelectMany(x => x.GetTypes());
        var modules = types
            .Where(t =>
                !t.IsAbstract &&
                t.IsNotPublic &&
                typeof(ICarterModule).IsAssignableFrom(t))
            .ToArray();

        services.AddCarter(configurator: c => c.WithModules(modules));

        return services;
    }
}

internal sealed class DependencyContextAssemblyCatalogCustom(params Assembly[] assemblies) : DependencyContextAssemblyCatalog
{
    public override IReadOnlyCollection<Assembly> GetAssemblies() => assemblies;
}
