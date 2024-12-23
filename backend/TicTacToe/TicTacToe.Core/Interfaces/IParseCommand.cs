namespace TicTacToe.Core.Interfaces;

public interface IParseCommand
{
    ICommand? CommandParse(string? input);
}