using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicTacToe.Infrastructure.Auth;
using TicTacToe.Infrastructure.DataBase;

namespace TicTacToe.Api.Extensions;

/// <summary>
///     Provides extension methods for registering core services in the DI container.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration["Database:ConnectionString"],
                b => b.MigrationsAssembly("TicTacToe.Infrastructure"));
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
}