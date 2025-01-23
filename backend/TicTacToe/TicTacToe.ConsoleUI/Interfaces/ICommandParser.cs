using TicTacToe.Application.Interfaces;

namespace TicTacToe.ConsoleUI.Interfaces;

public interface ICommandParser
{
    ICommand? CommandParse(string? input);
}