using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

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
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<IGameProcessor, GameProcessor>();
        services.AddScoped<ICommandInvoker, CommandInvoker>();
        services.AddScoped<IMiniMaxAi, MiniMaxAi>();
        services.AddSingleton<GameService>();
    }
}