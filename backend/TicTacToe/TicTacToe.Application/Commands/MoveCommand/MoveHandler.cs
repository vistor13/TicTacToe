using ErrorOr;
using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Application.Commands.MoveCommand;

public class MoveHandler(IGameProcessor gameProcessor, IGameRepository gameRepository)
    : IRequestHandler<MoveCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        var gameStateEntity = await gameRepository.GetById(request.GameId);

        if (gameStateEntity is null)
            return Error.NotFound(
                "NotFoundGame",
                "Game not found."
            );

        gameProcessor.LoadGameState(GameStateModel.ToModel(gameStateEntity));

        var gameStateModel = gameProcessor.GetGameState();

        var move = new MoveParametersDto(request.Row - 1, request.Col - 1, gameStateModel.CurrentPlayer.ToString());

        var result = gameProcessor.MakeMove(move);

        switch (result.IsError)
        {
            case true:
                return result.Errors;
            case false when gameStateModel is { IsRunning: true, ShouldAiMove: true }:
                gameProcessor.AiMakeMove(out _);
                break;
        }

        var gameStateDto = gameProcessor.GetGameState();

        await gameRepository.Update(request.GameId, GameStateModel.ToEntity(gameStateDto, gameStateEntity));

        return Result.Success;
    }
}