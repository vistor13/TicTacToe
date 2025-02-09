using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.Infrastructure.DataBase;

namespace TicTacToe.Api.Extensions;

/// <summary>
///     Provides extension methods for registering core services in the DI container.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    ///     Registers core services.
    /// </summary>
    /// <param name="services"></param>
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGameProcessor, GameProcessor>();
        services.AddScoped<ICommandInvoker, CommandInvoker>();
        services.AddScoped<IMiniMaxAi, MiniMaxAi>();
        services.AddSingleton<IGameStateManager, GameStateManager>();
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration["Database:ConnectionString"],
                b => b.MigrationsAssembly("TicTacToe.Infrastructure"));
        });
        return services;
    }
}