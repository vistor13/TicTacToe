using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class AiGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        gameProcessor.InitializeGame(false);
        consoleRenderer.RenderMessage(Constants.Messages.GameProcess.WelcomeMessageGameWithAi);
        return null;
    }
}