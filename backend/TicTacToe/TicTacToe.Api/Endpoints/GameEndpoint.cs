using TicTacToe.Api.Extensions;
using TicTacToe.Api.GameModels;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Api.Endpoints;

public static class GameEndpoint
{
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        var endPoints =
            app.MapGroup("/api/game/")
                .WithTags("Game");

        endPoints.MapPost(
            "start",
            (GameService gameService, IGameProcessor gameProcessor)
                => StartGame(gameProcessor, gameService));

        endPoints.MapPost(
            "move",
            (Guid gameId, GameService gameService, int row, int col, IGameProcessor gameProcessor)
                => MakeMove(gameService, gameId, gameProcessor, row, col));

        endPoints.MapGet(
            "state",
            (Guid gameId, GameService gameService) => GetGameState(gameService, gameId));
    }

    private static IResult GetGameState(GameService gameService, Guid gameId)
    {
        var gameState = gameService.GetGame(gameId);
        if (gameState == null) return Results.BadRequest("Game not found.");

        var resultState = StateViewModel.ToViewModel(gameState);

        return Results.Ok(resultState);
    }

    private static IResult MakeMove(GameService gameService, Guid gameId, IGameProcessor gameProcessor, int row,
        int col)
    {
        var gameState = gameService.GetGame(gameId);
        if (gameState == null) return Results.BadRequest("Game not found.");

        gameProcessor.LoadGameState(gameState);

        var currentPlayer = gameProcessor.GetBoard().CurrentTurn;

        var move = new MoveParameters(row - 1, col - 1, currentPlayer);

        var result = gameProcessor.MakeMove(move);

        gameService.SaveGame(gameId, gameProcessor.GetGameState());
        return result.ToResult();
    }

    private static IResult StartGame(IGameProcessor gameProcessor, GameService gameService)
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
    }
}