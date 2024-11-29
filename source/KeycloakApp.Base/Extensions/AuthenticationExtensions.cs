using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KeycloakApp.Base.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = configuration["Authentication:Audience"]!;
                options.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //IssuerSigningKey = new JsonWebKey(key),
                    //SignatureValidator = (token, _) => new JsonWebToken(token),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:ValidIssuer"]!,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:Audience"]!,

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // Clock skew compensates for server time drift
                    ClockSkew = TimeSpan.FromMinutes(1),
                };
            });

        return services;
    }
}
