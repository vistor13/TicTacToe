using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(
            string.Format(Messages.GameProcess.RestartNotification, gameProcessor.CurrentTurn));
        return Result.Success;
    }
}