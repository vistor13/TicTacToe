using TicTacToe.Application.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Interfaces;

public interface IMiniMaxAi
{
    MoveParametersDto FindBestMove(Board board);
}