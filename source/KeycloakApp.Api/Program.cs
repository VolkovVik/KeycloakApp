using System.Security.Claims;
using Carter;
using KeycloakApp.Api.Extensions;
using KeycloakApp.Application;
using KeycloakApp.Base.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddAuthenticationInternal(builder.Configuration);

builder.AddOpentelemetryInternal(KeycloakApp.Api.AssemblyReference.Name);

builder.Services.AddApplicationServices(configuration);

builder.Services.AddCarterConfiguration(KeycloakApp.Api.AssemblyReference.Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("test", (ClaimsPrincipal claimsPrincipal) =>
{
    return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

app.MapGet("api/name", (ILogger<Program> logger) =>
{
    logger.LogInformation("Name: {$Name}", KeycloakApp.Api.AssemblyReference.Name);
    DiagnosticConfig.Counter.Add(
        1,
        new KeyValuePair<string, object?>("value1", KeycloakApp.Api.AssemblyReference.Name),
        new KeyValuePair<string, object?>("value2", KeycloakApp.Api.AssemblyReference.Name));
    return KeycloakApp.Api.AssemblyReference.Name;
});

app.MapCarter();

#pragma warning disable S125 // Sections of code should not be commented out
// When use YARP Reverse Proxy appear error 307
// app.UseHttpsRedirection();
#pragma warning restore S125 // Sections of code should not be commented out

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();
