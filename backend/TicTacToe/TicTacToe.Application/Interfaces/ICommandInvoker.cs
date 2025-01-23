using ErrorOr;

namespace TicTacToe.Application.Interfaces;

public interface ICommandInvoker
{
    ErrorOr<Success> Execute(ICommand command);
}