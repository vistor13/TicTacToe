using TicTacToe.Core.Models;

namespace TicTacToe.Core.Validators
{
	public class OwnerCellValidator : MoveValidator
	{
		public override bool ValidateMove(int row, int col, GameProcessor game,PlayerTurn playerTurn)
		{
			if (game.GameBoard.Grid[row,col]!=' ')
			{
				return false;
			}
			return Next?.ValidateMove(row, col, game, playerTurn) ?? true;
		}
	}
}
