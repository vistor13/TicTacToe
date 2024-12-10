
using TicTacToe.Core.Models;

namespace TicTacToe.Core.States
{
	public class PlayerYState : State
	{
		public override bool MakeMove(int row, int col, GameProcessor game, PlayerTurn playerTurn)
		{
			if (!game.IsValidMove(row, col, playerTurn))
			{
				return false;
			}
			game.GameBoard.Grid[row, col] = 'Y';
			game.CurrentTurn = PlayerTurn.X;
			game.SwitchTurn();
			return true;
		}
	}
}
