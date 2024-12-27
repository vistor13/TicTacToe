using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public OperationResult Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(
            string.Format(Messages.GameProcess.RestartNotification, gameProcessor.CurrentTurn));
        return OperationResult.Success();
    }
}