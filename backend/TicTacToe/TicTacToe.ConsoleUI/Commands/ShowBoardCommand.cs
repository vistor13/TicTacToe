using ErrorOr;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class ShowBoardCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        var board = gameProcessor.GetBoard();
        renderer.RenderBoard(board.Grid);
        return Result.Success;
    }
}