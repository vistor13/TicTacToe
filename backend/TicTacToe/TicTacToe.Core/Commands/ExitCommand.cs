using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class ExitCommand : ICommand
{
    public bool Execute()
    {
        Environment.Exit(0);
        return true;
    }
}