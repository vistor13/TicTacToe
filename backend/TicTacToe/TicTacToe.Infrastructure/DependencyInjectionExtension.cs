using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Infrastructure.Auth;
using TicTacToe.Infrastructure.DataBase.Repositories;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();
    }

    public static void ConfigureAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Auth0Options>(configuration.GetSection("Auth0"));
    }
}