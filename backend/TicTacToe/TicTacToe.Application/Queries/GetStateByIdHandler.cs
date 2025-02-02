using ErrorOr;
using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Queries;

public class GetStateByIdHandler(IGameStateManager gameStateManager)
    : IRequestHandler<GetStateByIdQuery, ErrorOr<GameStateDto>>
{
    public Task<ErrorOr<GameStateDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        var gameState = gameStateManager.GetGame(request.Id);

        if (gameState == null)
            return Task.FromResult<ErrorOr<GameStateDto>>(Error.NotFound(
                "NotFoundGame",
                "Game not found."
            ));

        var gameStateDto = new GameStateDto(
            gameState.Modes.ToString(),
            gameState.CurrentPlayer.ToString(),
            gameState.State.ToString(),
            gameState.Grid,
            gameState.State is GameState.Ongoing,
            gameState.Modes is GameModes.GameWithAi);

        return Task.FromResult<ErrorOr<GameStateDto>>(gameStateDto);
    }
}