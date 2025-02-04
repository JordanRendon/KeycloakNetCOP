using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class JwtAuthenticationConfig
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Keycloak:Authority"];
                options.Audience = configuration["Keycloak:Audience"];
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Keycloak:Audience"],
                    ValidateLifetime = true
                };

                // 👇 Configurar la lectura de roles desde "realm_access"
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claims = context.Principal!.Identities.First().Claims.ToList();
                        var realmRoles = claims
                            .Where(c => c.Type == "realm_access")
                            .SelectMany(c => System.Text.Json.JsonDocument.Parse(c.Value)
                                .RootElement.GetProperty("roles")
                                .EnumerateArray()
                                .Select(role => role.GetString()))
                            .ToList();

                        var appIdentity = new System.Security.Claims.ClaimsIdentity();
                        foreach (var role in realmRoles)
                        {
                            appIdentity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role!));
                        }
                        context.Principal.AddIdentity(appIdentity);

                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
    }
}

