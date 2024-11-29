using System.Reflection;

namespace KeycloakApp.Users;

internal static class AssemblyReference
{
    private const string AssemblyName = "KeycloakApp.Users";

    internal static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    internal static readonly string Name = typeof(AssemblyReference).Assembly.GetName().Name ?? AssemblyName;
}
