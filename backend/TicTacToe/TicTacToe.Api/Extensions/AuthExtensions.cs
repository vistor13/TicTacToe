using System.Security.Claims;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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

    public static IServiceCollection AddAuth0ManagementApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IManagementApiClient>(provider =>
        {
            var config = provider.GetRequiredService<IOptions<Auth0Options>>().Value;

            var auth0AuthClient = new AuthenticationApiClient(new Uri($"https://{config.Domain}/"));

            var tokenResponse = auth0AuthClient.GetTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = config.ClientId,
                ClientSecret = config.ClientSecret,
                Audience = $"https://{config.Domain}/api/v2/"
            }).Result;

            var auth0Client =
                new ManagementApiClient(tokenResponse.AccessToken, new Uri($"https://{config.Domain}/api/v2/"));

            return auth0Client;
        });

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    internal static void AddAuthorization(
        this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy("admin", policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim("permissions", "access:full"))
            .AddPolicy("developer", policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim("permissions", "access:full"))
            .AddPolicy("player", policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim("permissions", "create:game"));
    }
}