using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Validators
{
	public class OwnerCellValidator : IValidator
	{
		public bool Validate(MoveParameters moveParameters, Board board)
		{
			if (board.Grid[moveParameters.Row, moveParameters.Col] != ' ')
			{
				return false;
			}
			return true;
		}

	}
}
