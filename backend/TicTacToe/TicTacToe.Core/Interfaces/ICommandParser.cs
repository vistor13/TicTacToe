namespace TicTacToe.Core.Interfaces;

public interface ICommandParser
{
    ICommand? CommandParse(string? input);
}