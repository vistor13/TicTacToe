using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Commands;

public class MoveCommand(GameProcessor gameProcessor, MoveParameters moveParameters) : ICommand
{
    public bool Execute()
    {
        return gameProcessor.MakeMove(moveParameters);
    }
}