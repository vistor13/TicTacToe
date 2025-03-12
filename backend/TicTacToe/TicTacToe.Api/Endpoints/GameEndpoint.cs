using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Contracts.Responses;
using TicTacToe.Api.Extensions;
using TicTacToe.Application.Commands.MoveCommand;
using TicTacToe.Application.Commands.StartGameCommand;
using TicTacToe.Application.Queries;

namespace TicTacToe.Api.Endpoints;

/// <summary>
///     Represent game endpoints.
/// </summary>
public static class GameEndpoint
{
    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var endPoints = app.MapGroup("/api/game/").WithTags("Game");

        endPoints.MapPost("start", StartGame);

        endPoints.MapPost("move", MakeMove);

        endPoints.MapGet("state", GetGameState);
    }

    [Authorize]
    private static async Task<IResult> StartGame(
        [FromQuery] bool isTwoPlayerMode,
        [FromServices] IMediator mediator)
    {
        var game = await mediator.Send
            (new StartGameCommand(isTwoPlayerMode));

        var gameResponse = new GameResponse(game.Id, game.Modes);

        return Results.Created("/api/game/start", gameResponse);
    }

    [Authorize]
    private static async Task<IResult> MakeMove(
        [FromBody] MoveRequest moveRequest,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send
            (new MoveCommand(moveRequest.GameId, moveRequest.Row, moveRequest.Col));

        return result.ToResult();
    }

    [Authorize]
    private static async Task<IResult> GetGameState(
        [FromQuery] long gameId,
        [FromServices] IMediator mediator)
    {
        var gameState = await mediator.Send
            (new GetStateByIdQuery(gameId));

        if (gameState.IsError)
            return Results.NotFound(gameState.Errors);

        var resultState = GameStateResponse.ToViewModel(gameState.Value);

        return Results.Ok(resultState);
    }
}