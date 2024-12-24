using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class OwnerCellValidator : IValidator
    {
        public bool Validate(MoveParameters moveParameters, Board board)
        {
            return board.Grid[moveParameters.Row, moveParameters.Col] == Board.EmptyCell;
        }
    }
}