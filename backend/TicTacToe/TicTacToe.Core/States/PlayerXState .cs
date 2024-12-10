using TicTacToe.Core.Models;

namespace TicTacToe.Core.States
{
	public class PlayerXState : State
	{
		public override bool MakeMove(int row, int col, GameProcessor game,PlayerTurn playerTurn)
		{
			if (!game.IsValidMove(row, col, playerTurn))
			{
				return false;
			}
			game.GameBoard.Grid[row, col] = 'X';
			game.CurrentTurn = PlayerTurn.Y;
			game.SwitchTurn();
			return true;
		}
	}
}
