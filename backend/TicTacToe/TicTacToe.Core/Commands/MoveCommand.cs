using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class MoveCommand(IGameProcessor gameProcessor, MoveParameters moveParameters) : ICommand
{
    public bool Execute()
    {
        return gameProcessor.MakeMove(moveParameters);
    }
}