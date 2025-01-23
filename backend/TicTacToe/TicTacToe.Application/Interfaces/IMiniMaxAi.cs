using TicTacToe.Core.Models;

namespace TicTacToe.Application.Interfaces;

public interface IMiniMaxAi
{
    MoveParameters FindBestMove(Board board);
}