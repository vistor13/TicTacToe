using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Api.Game;

public static class GameModule
{
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/game/start", (IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();

                return Results.Created("/api/game/start", gameProcessor);
            })
            .WithTags("Game")
            .Produces<GameProcessor>(StatusCodes.Status201Created);
    }
}