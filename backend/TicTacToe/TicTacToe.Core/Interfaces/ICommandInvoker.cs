using ErrorOr;

namespace TicTacToe.Core.Interfaces;

public interface ICommandInvoker
{
    ErrorOr<Success> Execute(ICommand command);
}