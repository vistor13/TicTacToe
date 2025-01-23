using ErrorOr;

namespace TicTacToe.Application.Interfaces;

public interface ICommand
{
    ErrorOr<Success> Execute();
}