using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Commands;

public class StartCommand(GameProcessor gameProcessor) : ICommand
{
    public bool Execute()
    {
        gameProcessor.InitializeGame();
        return true;
    }
}