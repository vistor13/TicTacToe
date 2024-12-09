using TicTacToe.Core.Models;

namespace TicTacToe.Core.Validators
{
	public class BoundsValidator : MoveValidator
	{
		public override bool ValidateMove(int row, int col, GameProcessor game,PlayerTurn playerTurn)
		{
			if (row < 0 || row >= game.GameBoard.Grid.GetLength(0) ||
		        col < 0 || col >= game.GameBoard.Grid.GetLength(1))
			{
				return false;
			}
			return Next?.ValidateMove(row, col, game, playerTurn) ?? true;
		}
	}
}
