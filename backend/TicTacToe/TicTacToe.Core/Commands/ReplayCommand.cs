using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public bool Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage($"Game restarted!!!! The player {gameProcessor.CurrentTurn} makes a move. ");
        return true;
    }
}