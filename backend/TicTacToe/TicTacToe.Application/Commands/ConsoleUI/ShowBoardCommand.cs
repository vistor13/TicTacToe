using ErrorOr;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Application.Commands.ConsoleUI;

public class ShowBoardCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        var board = gameProcessor.GetBoard();
        renderer.RenderBoard(board);
        return Result.Success;
    }
}