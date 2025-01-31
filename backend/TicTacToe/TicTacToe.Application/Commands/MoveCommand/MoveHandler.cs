using ErrorOr;
using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Commands.MoveCommand;

public class MoveHandler(IGameProcessor gameProcessor, IGameStateManager gameStateManager)
    : IRequestHandler<MoveCommand, ErrorOr<Success>>
{
    public Task<ErrorOr<Success>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        var gameState = gameStateManager.GetGame(request.GameId);

        if (gameState == null)
            return Task.FromResult<ErrorOr<Success>>(Error.NotFound(
                "NotFoundGame",
                "Game not found."
            ));

        gameProcessor.LoadGameState(gameState);

        var gameStateModel = GameStateModel.MapToModel(gameProcessor.GetGameState());

        var move = new MoveParameters(request.Row - 1, request.Col - 1, gameStateModel.CurrentPlayer);

        var result = gameProcessor.MakeMove(move);

        switch (result.IsError)
        {
            case true:
                return Task.FromResult<ErrorOr<Success>>(result.Errors);
            case false when gameStateModel is { Modes: GameModes.GameWithAi, State: GameState.Ongoing }:
                gameProcessor.AiMakeMove(out _);
                break;
        }

        gameStateManager.SaveGame(request.GameId, GameStateModel.MapToModel(gameProcessor.GetGameState()));

        return Task.FromResult<ErrorOr<Success>>(Result.Success);
    }
}