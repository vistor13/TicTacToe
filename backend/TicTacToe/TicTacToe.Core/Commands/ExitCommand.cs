using ErrorOr;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ExitCommand : ICommand
{
    public ErrorOr<Success> Execute()
    {
        Environment.Exit(0);
        return Result.Success;
    }
}