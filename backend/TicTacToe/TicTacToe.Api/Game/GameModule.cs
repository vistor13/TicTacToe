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

                return Results.Created("/api/game/start", gameProcessor);
            })
            .WithTags("Game");


        app.MapPost("/api/game/move", (int row, int col, IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();
                var currentPlayer = gameProcessor.GetBoard().CurrentTurn;
                var move = new MoveParameters(row - 1, col - 1, currentPlayer);

                var result = gameProcessor.MakeMove(move);

                if (result.IsError) return Results.BadRequest(result.Errors.First().Description);

                var board = gameProcessor.GetBoard();
                var gridList = Enumerable.Range(0, board.Grid.GetLength(0))
                    .Select(i => Enumerable.Range(0, board.Grid.GetLength(1))
                        .Select(j => board.Grid[i, j])
                        .ToList())
                    .ToList();

                var resultBoard = new
                {
                    Grid = gridList, board.CurrentTurn
                };

                return Results.Ok(resultBoard);
            })
            .WithTags("Game");

        app.MapGet("/api/game/state", (IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();
                var board = gameProcessor.GetBoard();
                var resultState = new StateViewModel
                {
                    State = board.State,
                    GameMode = gameProcessor.GameMode,
                    Grid = new List<List<char>>(),
                    PlayerTurn = board.CurrentTurn
                };

                return Results.Ok(resultState);
            })
            .WithTags("Game");
    }
}