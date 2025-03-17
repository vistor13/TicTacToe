using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;

namespace TicTacToe.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IGameProcessor, GameProcessor>();
        services.AddScoped<ICommandInvoker, CommandInvoker>();
        services.AddScoped<IMiniMaxAi, MiniMaxAi>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddScoped<IAuthService, AuthService>();
    }
}