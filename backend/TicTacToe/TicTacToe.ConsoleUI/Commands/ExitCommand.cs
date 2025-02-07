using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class ExitCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        gameProcessor.Reset();
        renderer.RenderMessage(Constants.Messages.GameProcess.GameModeSelection);
        return null;
    }
}