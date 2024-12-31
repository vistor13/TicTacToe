using ErrorOr;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class MoveCommand(IGameProcessor gameProcessor, MoveParameters moveParameters) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        return gameProcessor.MakeMove(moveParameters);
    }
}