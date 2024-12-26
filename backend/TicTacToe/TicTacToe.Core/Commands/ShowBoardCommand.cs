using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Commands;

public class ShowBoardCommand(GameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public bool Execute()
    {
        var board = gameProcessor.GetBoard();
        renderer.RenderBoard(board);
        return true;
    }
}