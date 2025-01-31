using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Commands;

public class MakeMoveCommand(IGameProcessor gameProcessor, MoveParameters moveParameters) : ICommand
{
    public ErrorOr<GameStateDto?>? Execute()
    {
        return gameProcessor.MakeMove(moveParameters);
    }
}