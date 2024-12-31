using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame(gameProcessor.GameMode != GameModes.GameWithAi);
        consoleRenderer.RenderMessage(
            string.Format(Messages.GameProcess.RestartNotification, gameProcessor.CurrentTurn));
        return Result.Success;
    }
}