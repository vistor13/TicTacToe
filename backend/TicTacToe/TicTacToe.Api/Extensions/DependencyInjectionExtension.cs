using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IGameProcessor, GameProcessor>();
        services.AddScoped<ICommandInvoker, CommandInvoker>();
        services.AddScoped<IMiniMaxAi, MiniMaxAi>();
        return services;
    }
}