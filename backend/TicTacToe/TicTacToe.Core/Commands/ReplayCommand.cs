using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(GameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public bool Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage($"Game restarted!!!! The player {gameProcessor.CurrentTurn} makes a move. ");
        return true;
    }
}