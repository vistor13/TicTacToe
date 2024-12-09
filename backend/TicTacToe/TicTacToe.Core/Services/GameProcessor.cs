using TicTacToe.Core.Validators;

namespace TicTacToe.Core.Models
{
	public enum GameState
	{
		Ongoing,
		Draw,
		PlayerOneWin,
		PlayerTwoWin
	}
	public enum PlayerTurn
	{
		X,
		Y
	}
	public class GameProcessor
	{
		public Board GameBoard { get; set; }
		public GameState State { get; set; }
		public PlayerTurn CurrentTurn{ get; set; }
		private MoveValidator validationChain { get; set; }
		public GameProcessor() 
		{
			GameBoard= new Board();
			State = GameState.Ongoing;
			CurrentTurn = PlayerTurn.X;
			InitializeValidationChain();
		}
		private void InitializeValidationChain()
		{
			var boundsValidator = new BoundsValidator();
			var ownerCellValidator = new OwnerCellValidator();
			var playerCurrentValidator = new PlayerCurrentValidator();

			playerCurrentValidator.SetNext(boundsValidator)
								  .SetNext(ownerCellValidator);

			validationChain = playerCurrentValidator;
		}

		public bool IsValidMove(int row, int col,PlayerTurn playerTurn)
		{
			return validationChain.ValidateMove(row, col, this,playerTurn);
		}

	}
	
}
