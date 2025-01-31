using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.Commands;

public class ReplayCommand(
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        var gameState = gameProcessor.GetGameState();
        gameProcessor.InitializeGame(gameProcessor.GameMode != GameModes.GameWithAi);
        consoleRenderer.RenderMessage(
            string.Format(Constants.Messages.GameProcess.RestartNotification, gameState.CurrentPlayer));
        return null;
    }
}