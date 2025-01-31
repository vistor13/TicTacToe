using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class PlayerGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(Constants.Messages.GameProcess.WelcomeMessageGameWithPlayer);
        return null;
    }
}