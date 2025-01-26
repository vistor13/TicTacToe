using ErrorOr;
using MediatR;
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

        var currentPlayer = gameProcessor.GetBoard().CurrentTurn;

        var move = new MoveParameters(request.Row - 1, request.Col - 1, currentPlayer);

        var result = gameProcessor.MakeMove(move);

        if (!result.IsError && gameState is { GameModes: GameModes.GameWithAi, State: GameState.Ongoing })
            gameProcessor.AiMakeMove(out _);

        gameStateManager.SaveGame(request.GameId, gameProcessor.GetGameState());

        return Task.FromResult(result);
    }
}