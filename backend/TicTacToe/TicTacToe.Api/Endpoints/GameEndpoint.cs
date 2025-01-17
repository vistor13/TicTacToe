using TicTacToe.Api.Extensions;
using TicTacToe.Api.Game;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Api.Endpoints;

public static class GameEndpoint
{
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/game/start", (GameService gameService, IGameProcessor gameProcessor) =>
            {
                gameProcessor.InitializeGame();

                var gameState = gameProcessor.GetGameState();

                var gameId = Guid.NewGuid();
                gameService.SaveGame(gameId, gameState);

                var gameViewModel = new GameViewModel
                {
                    Id = gameId,
                    GameMode = gameState.GameMode
                };

                return Results.Created("/api/game/start", gameViewModel);
            })
            .WithTags("Game");

        app.MapPost("/api/game/move",
                (Guid gameId, GameService gameService, int row, int col, IGameProcessor gameProcessor) =>
                {
                    var gameState = gameService.GetGame(gameId);
                    if (gameState == null) return Results.BadRequest("Game not found.");

                    gameProcessor.LoadGameState(gameState);

                    var currentPlayer = gameProcessor.GetBoard().CurrentTurn;

                    var move = new MoveParameters(row - 1, col - 1, currentPlayer);

                    var result = gameProcessor.MakeMove(move);

                    gameService.SaveGame(gameId, gameProcessor.GetGameState());
                    return result.ToResult();
                })
            .WithTags("Game");

        app.MapGet("/api/game/state", (Guid gameId, GameService gameService, IGameProcessor gameProcessor) =>
            {
                var game = gameService.GetGame(gameId);
                if (game == null) return Results.BadRequest("Game not found.");

                gameProcessor.LoadGameState(game);

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