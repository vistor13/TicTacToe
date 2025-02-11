using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Infrastructure.Entities;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Application.Commands.StartGameCommand;

public class StartGameHandler(IGameProcessor gameProcessor, IGameRepository gameRepository)
    : IRequestHandler<StartGameCommand, GameInitializationDto>
{
    public async Task<GameInitializationDto> Handle(StartGameCommand request,
        CancellationToken cancellationToken)
    {
        gameProcessor.InitializeGame(request.IsTwoPlayerMode);

        var gameState = gameProcessor.GetGameState();

        var entity = new GameEntity
        {
            GameState = gameState.State,
            Mode = gameState.Modes,
            CurrentPlayer = gameState.CurrentPlayer,
            Moves = []
        };

        var createEntity = await gameRepository.Create(entity);
        return new GameInitializationDto(createEntity.Id, gameState.Modes.ToString());
    }
}