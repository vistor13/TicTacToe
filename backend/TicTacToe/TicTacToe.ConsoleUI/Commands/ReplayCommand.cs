using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class ReplayCommand(
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        var gameState = gameProcessor.GetGameState();
        gameProcessor.InitializeGame(!gameProcessor.ShouldAiMove);
        consoleRenderer.RenderMessage(
            string.Format(Constants.Messages.GameProcess.RestartNotification, gameState.CurrentPlayer));
        return null;
    }
}