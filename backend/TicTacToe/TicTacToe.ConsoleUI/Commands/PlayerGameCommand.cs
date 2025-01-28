using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class PlayerGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(Messages.GameProcess.WelcomeMessageGameWithPlayer);
        return Result.Success;
    }
}