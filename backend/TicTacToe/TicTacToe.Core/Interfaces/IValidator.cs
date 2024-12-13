using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces
{
	public interface IValidator
	{
		bool Validate(MoveParameters moveParameters, Board board);
	}
}
