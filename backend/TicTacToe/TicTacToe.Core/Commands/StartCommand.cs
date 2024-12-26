using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Commands;

public class StartCommand(GameProcessor? gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public bool Execute()
    {
        gameProcessor!.InitializeGame();
        consoleRenderer.RenderMessage($"The game is started, the player {gameProcessor.CurrentTurn} makes a move.");
        return true;
    }
}