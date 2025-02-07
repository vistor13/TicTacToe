using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands;

public class MakeMoveCommand(IGameProcessor gameProcessor, MoveParametersDto moveParameters) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        return gameProcessor.MakeMove(moveParameters);
    }
}