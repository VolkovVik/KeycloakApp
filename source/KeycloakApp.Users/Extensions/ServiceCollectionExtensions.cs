using Microsoft.OpenApi.Models;

namespace KeycloakApp.Users.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            options.CustomSchemaIds(t => t.FullName?.Replace('+', '.'));

            ///options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            ///{
            ///    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            ///    Name = "Authorization",
            ///    In = ParameterLocation.Header,
            ///    Type = SecuritySchemeType.ApiKey,
            ///    Scheme = "Bearer"
            ///});

            options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" },
                            { "profile", "profile" },
                            { "email", "email" }
                        }
                    }
                }
            });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Keycloak",
                            Type = ReferenceType.SecurityScheme
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer"
                    },
                    Array.Empty<string>()
                }
            };
            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
