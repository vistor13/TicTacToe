using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Application.Commands.StartGameCommand;

public class StartGameHandler(IGameProcessor gameProcessor, IGameRepository gameRepository)
    : IRequestHandler<StartGameCommand, GameInitializationDto>
{
    public async Task<GameInitializationDto> Handle(StartGameCommand request,
        CancellationToken cancellationToken)
    {
        gameProcessor.InitializeGame(request.IsTwoPlayerMode);

        var createEntity = await gameRepository.Create(GameStateModel.ToEntity(gameProcessor.GetGameState()));
        return new GameInitializationDto(createEntity.Id, createEntity.Mode.ToString());
    }
}