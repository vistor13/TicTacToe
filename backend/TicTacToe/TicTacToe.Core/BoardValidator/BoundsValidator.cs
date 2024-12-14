using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
	public class BoundsValidator : IValidator
	{
		private const int RowDimension = 0;

		private const int ColumnDimension = 1;

		private const int LowerBound = 0;
		public bool Validate(MoveParameters moveParameters, Board board)
		{
			return moveParameters.Row >= LowerBound && moveParameters.Row < board.Grid.GetLength(RowDimension) &&
			       moveParameters.Col >= LowerBound && moveParameters.Row < board.Grid.GetLength(ColumnDimension);
		}
		
	}
}
