using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class StartCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public OperationResult Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage($"The game is started, the player {gameProcessor.CurrentTurn} makes a move.");
        return OperationResult.Success();
    }
}