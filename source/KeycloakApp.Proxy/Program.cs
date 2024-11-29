using KeycloakApp.Base.Extensions;
using KeycloakApp.Proxy;
using KeycloakApp.Proxy.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRateLimiting();

builder.Services.AddAuthenticationInternal(builder.Configuration);

builder.AddOpentelemetryInternal(AssemblyReference.Name);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapReverseProxy();

await app.RunAsync();
