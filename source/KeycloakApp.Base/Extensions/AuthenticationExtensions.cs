using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakApp.Base.Extensions;

public static class AuthenticationExtensions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
    public static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization();

        //    services.AddAuthentication(option =>
        //    {
        //        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //        .AddJwtBearer(options =>
        //        {
        //            options.RequireHttpsMetadata = false;
        //            options.Audience = configuration["Authentication:Audience"]!;
        //            options.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                //IssuerSigningKey = new JsonWebKey(key),
        //                //SignatureValidator = (token, _) => new JsonWebToken(token),

        //                // Validate the JWT Issuer (iss) claim
        //                ValidateIssuer = true,
        //                ValidIssuer = configuration["Authentication:ValidIssuer"]!,

        //                // Validate the JWT Audience (aud) claim
        //                ValidateAudience = true,
        //                ValidAudience = configuration["Authentication:Audience"]!,

        //                // Validate the token expiry
        //                ValidateLifetime = true,

        //                // Clock skew compensates for server time drift
        //                ClockSkew = TimeSpan.FromMinutes(1),
        //            };
        //        });

        services.AddAuthentication().AddJwtBearer();

        services.AddHttpContextAccessor();

        services.ConfigureOptions<JwtBearerConfigureOptions>();

        return services;
    }
}
