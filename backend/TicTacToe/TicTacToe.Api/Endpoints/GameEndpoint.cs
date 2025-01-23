using MediatR;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Contracts.Responses;
using TicTacToe.Api.Extensions;
using TicTacToe.Application.Commands.WebApi.StartGameCommand;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.Core.Models;

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
            (IMediator mediator,
                    StartGameCommand startGameCommand)
                => StartGame(mediator, startGameCommand));

        endPoints.MapPost(
            "move",
            (MoveRequest moveRequest,
                    IGameProcessor gameProcessor,
                    GameStateManager gameService)
                => MakeMove(gameService, gameProcessor, moveRequest));

        endPoints.MapGet(
            "state",
            (Guid gameId,
                    GameStateManager gameService)
                => GetGameState(gameService, gameId));
    }

    private static async Task<IResult> StartGame(IMediator mediator,
        StartGameCommand startGameCommand)
    {
        var game = await mediator.Send(startGameCommand);

        var gameResponse = new GameResponse
        {
            Id = game.Id,
            GameMode = game.Modes.ToString()
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