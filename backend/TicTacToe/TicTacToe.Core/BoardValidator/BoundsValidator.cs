using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
namespace TicTacToe.Core.Validators
{
	public class BoundsValidator : IValidator
	{
		private int RowDimension = 0;

		private int ColumnDimension = 1;

		private int LowerBound = 0;
		public bool Validate(MoveParameters moveParameters, Board board)
		{
			if (moveParameters.Row < LowerBound || moveParameters.Row >= board.Grid.GetLength(RowDimension) ||
				moveParameters.Col < LowerBound || moveParameters.Row >= board.Grid.GetLength(ColumnDimension))
			{
				return false;
			}
			return true;
		}
	}
}
