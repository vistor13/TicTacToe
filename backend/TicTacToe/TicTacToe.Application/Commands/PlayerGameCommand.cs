using ErrorOr;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Application.Commands;

public class PlayerGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame();
        consoleRenderer.RenderMessage(Messages.GameProcess.WelcomeMessageGameWithPlayer);
        return Result.Success;
    }
}