using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class StartCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public bool Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage($"The game is started, the player {gameProcessor.CurrentTurn} makes a move.");
        return true;
    }
}