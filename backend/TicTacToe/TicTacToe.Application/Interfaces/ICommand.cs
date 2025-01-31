using ErrorOr;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Interfaces;

public interface ICommand
{
    ErrorOr<GameStateDto>? Execute();
}