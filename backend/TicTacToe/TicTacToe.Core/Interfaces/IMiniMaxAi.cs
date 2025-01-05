using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IMiniMaxAi
{
    MoveParameters FindBestMove(Board board);
}