using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Commands;

public class ReplayCommand(
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame(gameProcessor.GameMode != GameModes.GameWithAi);
        consoleRenderer.RenderMessage(
            string.Format(Messages.GameProcess.RestartNotification, gameProcessor.GetBoard().CurrentTurn));
        return Result.Success;
    }
}