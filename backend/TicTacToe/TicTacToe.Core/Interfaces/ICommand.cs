using ErrorOr;

namespace TicTacToe.Core.Interfaces;

public interface ICommand
{
    ErrorOr<Success> Execute();
}