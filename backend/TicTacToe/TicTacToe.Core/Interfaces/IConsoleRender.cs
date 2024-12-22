using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IConsoleRenderer
{
    void RenderBoard(Board board);
    void RenderInstruction();
}