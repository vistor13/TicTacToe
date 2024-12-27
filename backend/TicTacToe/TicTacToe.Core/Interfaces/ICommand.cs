using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface ICommand
{
    OperationResult Execute();
}