using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class OwnerCellValidator : IValidator
    {
        public OperationResult Validate(MoveParameters moveParameters, Board board)
        {
            return board.Grid[moveParameters.Row, moveParameters.Col] == Board.EmptyCell
                ? OperationResult.Success()
                : OperationResult.Failure(Messages.Error.CellOccupied);
        }
    }
}