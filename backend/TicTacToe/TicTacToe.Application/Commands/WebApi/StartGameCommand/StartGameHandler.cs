using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;

namespace TicTacToe.Application.Commands.WebApi.StartGameCommand;

public class StartGameHandler(IGameProcessor gameProcessor, GameStateManager gameStateManager)
    : IRequestHandler<StartGameCommand, GameInitializationDto>
{
    public Task<GameInitializationDto> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        gameProcessor.InitializeGame(request.IsTwoPlayerMode);

        var gameState = gameProcessor.GetGameState();

        var gameId = Guid.NewGuid();

        gameStateManager.SaveGame(gameId, gameState);

        return Task.FromResult(new GameInitializationDto(gameId, gameState.GameModes));
    }
}