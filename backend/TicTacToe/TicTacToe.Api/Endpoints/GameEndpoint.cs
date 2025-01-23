using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Contracts.Responses;
using TicTacToe.Api.Extensions;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Api.Endpoints;

/// <summary>
///     Represent game endpoints.
/// </summary>
public static class GameEndpoint
{
    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        var endPoints =
            app.MapGroup("/api/game/")
                .WithTags("Game");

        endPoints.MapPost(
            "start",
            (GameService gameService,
                    IGameProcessor gameProcessor,
                    bool isTwoPlayersGame)
                => StartGame(gameProcessor, gameService, isTwoPlayersGame));

        endPoints.MapPost(
            "move",
            (MoveRequest moveRequest,
                    IGameProcessor gameProcessor,
                    GameService gameService)
                => MakeMove(gameService, gameProcessor, moveRequest));

        endPoints.MapGet(
            "state",
            (Guid gameId,
                    GameService gameService)
                => GetGameState(gameService, gameId));
    }

    private static IResult StartGame(IGameProcessor gameProcessor, GameService gameService,
        bool isTwoPlayersGame)
    {
        gameProcessor.InitializeGame(isTwoPlayersGame);

        var gameState = gameProcessor.GetGameState();

        var gameId = Guid.NewGuid();
        gameService.SaveGame(gameId, gameState);

        var gameResponse = new GameResponse
        {
            Id = gameId,
            GameMode = gameState.GameMode.ToString()
        };

        return Results.Created("/api/game/start", gameResponse);
    }

    private static IResult MakeMove(
        GameService gameService,
        IGameProcessor gameProcessor,
        MoveRequest moveRequest)
    {
        var gameState = gameService.GetGame(moveRequest.GameId);
        if (gameState == null) return Results.BadRequest("Game not found.");

        gameProcessor.LoadGameState(gameState);

        var currentPlayer = gameProcessor.GetBoard().CurrentTurn;

        var move = new MoveParameters(moveRequest.Row - 1, moveRequest.Col - 1, currentPlayer);

        var result = gameProcessor.MakeMove(move);

        if (!result.IsError && gameState is { GameMode: GameModes.GameWithAi, State: GameState.Ongoing })
            gameProcessor.AiMakeMove(out _);

        gameService.SaveGame(moveRequest.GameId, gameProcessor.GetGameState());
        return result.ToResult();
    }

    private static IResult GetGameState(GameService gameService, Guid gameId)
    {
        var gameState = gameService.GetGame(gameId);
        if (gameState == null) return Results.BadRequest("Game not found.");

        var resultState = GameStateResponse.ToViewModel(gameState);

        return Results.Ok(resultState);
    }
}