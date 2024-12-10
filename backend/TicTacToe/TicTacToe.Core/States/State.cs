
using TicTacToe.Core.Models;

namespace TicTacToe.Core.States
{
	public abstract class State
	{
		public abstract bool MakeMove(int row, int col, GameProcessor game,PlayerTurn playerTurn);
	}
}
