using TicTacToe.Api.Extensions;

namespace TicTacToe.Api;

/// <summary>
///     The entry point of the application
/// </summary>
public class Program
{
    internal static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApplicationServices();

        var app = builder.Build();

        app.UseApplicationMiddlewares();

        app.Run();
    }
}