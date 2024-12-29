using ErrorOr;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces
{
    public interface IValidator
    {
        ErrorOr<Success> Validate(MoveParameters moveParameters, Board board);
    }
}