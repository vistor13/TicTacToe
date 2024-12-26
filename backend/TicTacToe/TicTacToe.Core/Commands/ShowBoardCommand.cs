using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ShowBoardCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public bool Execute()
    {
        var board = gameProcessor.GetBoard();
        renderer.RenderBoard(board);
        return true;
    }
}