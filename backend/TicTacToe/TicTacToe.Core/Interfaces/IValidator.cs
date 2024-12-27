using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces
{
    public interface IValidator
    {
        OperationResult Validate(MoveParameters moveParameters, Board board);
    }
}