using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ExitCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.Reset();
        renderer.RenderMessage(Messages.GameProcess.GameModeSelection);
        return Result.Success;
    }
}