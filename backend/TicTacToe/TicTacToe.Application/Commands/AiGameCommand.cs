using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Application.Commands;

public class AiGameCommand(IGameProcessor gameProcessor, IUiRender consoleRenderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.InitializeGame(false);
        consoleRenderer.RenderMessage(Messages.GameProcess.WelcomeMessageGameWithAi);
        return Result.Success;
    }
}