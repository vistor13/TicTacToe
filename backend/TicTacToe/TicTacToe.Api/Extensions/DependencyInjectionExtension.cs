using Microsoft.EntityFrameworkCore;
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
}