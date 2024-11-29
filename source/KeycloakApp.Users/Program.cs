using KeycloakApp.Base.Extensions;
using KeycloakApp.Users;
using KeycloakApp.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddAuthenticationInternal(builder.Configuration);

builder.Services.AddOpentelemetryInternal(AssemblyReference.Name);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/name", () => AssemblyReference.Name);

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();
