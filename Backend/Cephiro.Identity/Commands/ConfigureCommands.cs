using System.Text;
using Cephiro.Identity.Commands.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Cephiro.Identity.Commands;

public static class ConfigureCommands
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(configuration);

        services.AddSingleton<DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var JwtSettings = new JwtConfig();
        configuration.Bind(JwtConfig.SectionName, JwtSettings);

        services.AddSingleton<JwtConfig>();
        services.AddSingleton<JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(JwtSettings.Secret!)
                        )
                    };
                });

        return services;
    }
}