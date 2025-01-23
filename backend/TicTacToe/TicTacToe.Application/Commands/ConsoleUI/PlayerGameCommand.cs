using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Application.Commands.ConsoleUI;

public class PlayerGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(Messages.GameProcess.WelcomeMessageGameWithPlayer);
        return Result.Success;
    }
}