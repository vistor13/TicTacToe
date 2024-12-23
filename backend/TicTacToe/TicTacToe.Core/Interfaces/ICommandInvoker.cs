namespace TicTacToe.Core.Interfaces;

public interface ICommandInvoker
{
    bool Execute(ICommand command);
}