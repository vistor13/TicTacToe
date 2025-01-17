using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class AiGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame(false);
        consoleRenderer.RenderMessage(Messages.GameProcess.WelcomeMessageGameWithAi);
        return Result.Success;
    }
}