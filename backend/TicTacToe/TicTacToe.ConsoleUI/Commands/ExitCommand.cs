using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class ExitCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.Reset();
        renderer.RenderMessage(Messages.GameProcess.GameModeSelection);
        return Result.Success;
    }
}