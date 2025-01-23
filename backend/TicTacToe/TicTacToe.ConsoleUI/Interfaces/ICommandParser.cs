using TicTacToe.Core.Interfaces;

namespace TicTacToe.ConsoleUI.Interfaces;

public interface ICommandParser
{
    ICommand? CommandParse(string? input);
}