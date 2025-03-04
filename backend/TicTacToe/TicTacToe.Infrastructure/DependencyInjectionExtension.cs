using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Infrastructure.DataBase.Repositories;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();
    }
}