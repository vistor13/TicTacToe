using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Services;

public class CommandInvoker : ICommandInvoker
{
    public bool Execute(ICommand command)
    {
        return command.Execute();
    }
}