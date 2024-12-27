using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ShowBoardCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public OperationResult Execute()
    {
        var board = gameProcessor.GetBoard();
        renderer.RenderBoard(board);
        return OperationResult.Success();
    }
}