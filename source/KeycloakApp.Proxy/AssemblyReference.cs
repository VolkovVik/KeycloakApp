using System.Reflection;

namespace KeycloakApp.Proxy;

internal static class AssemblyReference
{
    private const string AssemblyName = "KeycloakApp.Proxy";

    internal static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    internal static readonly string Name = typeof(AssemblyReference).Assembly.GetName().Name ?? AssemblyName;
}
