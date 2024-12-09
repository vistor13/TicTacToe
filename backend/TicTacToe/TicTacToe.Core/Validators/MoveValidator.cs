
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Validators
{
	public abstract class MoveValidator
	{
		protected MoveValidator Next;

		public MoveValidator SetNext(MoveValidator next)
		{
			Next = next;
			return next;
		}

		public abstract bool ValidateMove(int row,int col,Game game, PlayerTurn playerTurn);
	}
}
