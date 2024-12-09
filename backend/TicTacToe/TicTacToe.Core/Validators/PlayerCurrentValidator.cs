
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Validators
{
	public class PlayerCurrentValidator : MoveValidator
	{
		public override bool ValidateMove(int row, int col, 
			GameProcessor game, PlayerTurn playerTurn )
		{
			if (game.CurrentTurn != playerTurn)
				return false;
			return Next?.ValidateMove(row, col, game, playerTurn) ?? true;
		}
	}
}
