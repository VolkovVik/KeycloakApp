using System.Reflection;

namespace KeycloakApp.Base;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
