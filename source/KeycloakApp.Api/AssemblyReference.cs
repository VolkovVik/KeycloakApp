using System.Reflection;

namespace KeycloakApp.Api;

internal static class AssemblyReference
{
    private const string AssemblyName = "KeycloakApp.Api";

    internal static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    internal static readonly string Name = typeof(AssemblyReference).Assembly.GetName().Name ?? AssemblyName;
}
