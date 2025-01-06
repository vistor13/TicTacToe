using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class OwnerCellValidator : IValidator
    {
        public ErrorOr<Success> Validate(MoveParameters moveParameters, Board board)
        {
            return board.Grid[moveParameters.Row, moveParameters.Col] == Board.EmptyCell
                ? Result.Success
                : Error.Validation(
                    "CellOccupied",
                    Messages.Error.CellOccupied);
        }
    }
}