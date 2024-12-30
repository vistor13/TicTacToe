using ErrorOr;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class EndGameCommand : ICommand
{
    public ErrorOr<Success> Execute()
    {
        Environment.Exit(0);
        return Result.Success;
    }
}