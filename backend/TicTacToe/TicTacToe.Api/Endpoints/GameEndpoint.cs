using MediatR;
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
    public static void AddGameEndpoints(this IEndpointRouteBuilder app)
    {
        var endPoints =
            app.MapGroup("/api/game/")
                .WithTags("Game");

        endPoints.MapPost(
            "start",
            StartGame);

        endPoints.MapPost(
            "move",
            MakeMove);

        endPoints.MapGet(
            "state",
            GetGameState);
    }

    private static async Task<IResult> StartGame(IMediator mediator,
        bool isTwoPlayerMode)
    {
        var game = await mediator.Send
            (new StartGameCommand(isTwoPlayerMode));

        var gameResponse = new GameResponse
        {
            Id = game.Id,
            GameMode = game.Modes.ToString()
        };

        return Results.Created("/api/game/start", gameResponse);
    }

    private static async Task<IResult> MakeMove(
        MoveRequest moveRequest, IMediator mediator)
    {
        var result = await mediator.Send
            (new MoveCommand(moveRequest.GameId, moveRequest.Row, moveRequest.Col));

        return result.ToResult();
    }

    private static async Task<IResult> GetGameState(Guid gameId,
        IMediator mediator)
    {
        var gameState = await mediator.Send
            (new GetStateByIdQuery(gameId));

        if (gameState.IsError)
            return Results.NotFound(gameState.Errors);

        var resultState = GameStateResponse.ToViewModel(gameState.Value);

        return Results.Ok(resultState);
    }
}