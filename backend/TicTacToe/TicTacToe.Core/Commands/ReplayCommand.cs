using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public OperationResult Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage($"Game restarted!!!! The player {gameProcessor.CurrentTurn} makes a move. ");
        return OperationResult.Success();
    }
}