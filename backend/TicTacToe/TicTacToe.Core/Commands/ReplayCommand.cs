using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ReplayCommand(
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer,
    IGameStateService gameStateService) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame(gameStateService.GameMode != GameModes.GameWithAi);
        consoleRenderer.RenderMessage(
            string.Format(Messages.GameProcess.RestartNotification, gameStateService.CurrentTurn));
        return Result.Success;
    }
}