using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class OwnerCellValidator : IValidator
    {
        public OperationResult Validate(MoveParameters moveParameters, Board board)
        {
            if (board.Grid[moveParameters.Row, moveParameters.Col] == Board.EmptyCell) return OperationResult.Success();

            return OperationResult.Failure("The selected cell is already occupied. Please choose another cell.");
        }
    }
}