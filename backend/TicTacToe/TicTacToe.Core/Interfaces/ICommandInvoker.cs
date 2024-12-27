using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface ICommandInvoker
{
    OperationResult Execute(ICommand command);
}