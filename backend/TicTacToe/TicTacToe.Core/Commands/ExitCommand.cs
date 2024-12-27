using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class ExitCommand : ICommand
{
    public OperationResult Execute()
    {
        Environment.Exit(0);
        return OperationResult.Success();
    }
}