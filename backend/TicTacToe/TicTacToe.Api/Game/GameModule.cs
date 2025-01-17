using TicTacToe.Api.Extensions;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Api.Game;

public static class GameModule
{
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/game/start", (IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();
                var gameViewModel = new GameViewModel
                {
                    IsRunning = gameProcessor.IsRunning,
                    GameMode = gameProcessor.GameMode
                };

                return Results.Created("/api/game/start", gameViewModel);
            })
            .WithTags("Game");

        app.MapPost("/api/game/move", (int row, int col, IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();

                var currentPlayer = gameProcessor.GetBoard().CurrentTurn;

                var move = new MoveParameters(row - 1, col - 1, currentPlayer);

                var result = gameProcessor.MakeMove(move);

                return result.ToResult();
            })
            .WithTags("Game");

        app.MapGet("/api/game/state", (IGameProcessor gameProcessor) =>
            {
                var gameState = gameProcessor.GetGameState();

                var gridList = Enumerable.Range(0, Board.BoardSize)
                    .Select(i => Enumerable.Range(0, Board.BoardSize)
                        .Select(j => gameState.Grid[i, j])
                        .ToList())
                    .ToList();

                var resultState = new StateViewModel
                {
                    State = gameState.State,
                    GameMode = gameState.GameMode,
                    Grid = gridList,
                    PlayerTurn = gameState.PlayerTurn
                };

                return Results.Ok(resultState);
            })
            .WithTags("Game");
    }
}