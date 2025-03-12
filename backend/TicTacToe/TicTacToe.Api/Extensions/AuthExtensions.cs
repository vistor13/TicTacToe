using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Infrastructure.Auth;

namespace TicTacToe.Api.Extensions;

/// <summary>
/// </summary>
public static class AuthExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = configuration["Auth0:Authority"];
                opt.Audience = configuration["Auth0:Audience"];
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidAudience = configuration["Auth0:Audience"],
                    ValidIssuer = configuration["Auth0:Domain"]
                };
            });

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Auth0Options>(configuration.GetSection("Auth0"));
    }

    internal static void AddAuthorization(
        this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy("admin", policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim("permissions", "access:admin"));
    }
}