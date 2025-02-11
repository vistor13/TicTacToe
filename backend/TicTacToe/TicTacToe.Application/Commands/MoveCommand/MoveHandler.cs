using ErrorOr;
using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Infrastructure.DataBase.Specifications;
using TicTacToe.Infrastructure.Entities;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Application.Commands.MoveCommand;

public class MoveHandler(IGameProcessor gameProcessor, IGameRepository gameRepository, IMoveRepository moveRepository)
    : IRequestHandler<MoveCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        var gameStateEntity = await gameRepository.GetFirstBySpecification(new ByIdGameSpecification(request.GameId));

        if (gameStateEntity is null)
            return Error.NotFound(
                "NotFoundGame",
                "Game not found."
            );

        gameProcessor.LoadGameState(GameStateModel.MapToModel(gameStateEntity));


        var move = new MoveParametersDto(request.Row - 1, request.Col - 1, gameStateEntity.CurrentPlayer.ToString());

        var result = gameProcessor.MakeMove(move);

        var gameStateModel = gameProcessor.GetGameState();

        switch (result.IsError)
        {
            case true:
                return result.Errors;
            case false when gameStateModel is { IsRunning: true, ShouldAiMove: true }:
                gameProcessor.AiMakeMove(out _);
                break;
        }

        var gameModel = gameProcessor.GetGameState();

        var moveEntity = new MoveEntity
        {
            Row = request.Row,
            Col = request.Col,
            MoveSymbol = move.Player[0],
            GameId = gameStateEntity.Id
        };

        await gameRepository.Update(request.GameId, GameStateModel.MapToEntity(gameModel));
        await moveRepository.Create(moveEntity);
        return Result.Success;
    }
}