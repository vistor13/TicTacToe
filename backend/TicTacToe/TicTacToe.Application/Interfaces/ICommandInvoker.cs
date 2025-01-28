using ErrorOr;

namespace TicTacToe.Application.Interfaces;

public interface ICommandInvoker
{
    ErrorOr<Success> Execute(ICommand command, Dictionary<string, List<Type>> _commandsByState);
}