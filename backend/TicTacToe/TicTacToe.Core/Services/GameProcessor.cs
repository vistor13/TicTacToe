using TicTacToe.Core.States;
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
		private State CurrentState {  get; set; }

		public GameProcessor() 
		{
			GameBoard= new Board();
			State = GameState.Ongoing;
			CurrentTurn = PlayerTurn.X;
			CurrentState = new PlayerXState();
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
		public bool MakeMove(int row, int col, PlayerTurn player)
		{
			if (!CurrentState.MakeMove(row, col, this, player))
			{
				return false;
			}

			if (CheckWin(player))
			{
				State = player == PlayerTurn.X ? GameState.PlayerOneWin : GameState.PlayerTwoWin;
			}
			return true;
		}
		public void SwitchTurn()
		{
			CurrentState = CurrentState is PlayerXState
				? new PlayerYState()
				: new PlayerXState();
		}
		public bool CheckWin(PlayerTurn player)
		{
			char currentplayer = player == PlayerTurn.X ? 'X' : 'Y';
			int size = GameBoard.Grid.GetLength(0);

			for (int i = 0; i < size; i++) 
			{
				if (CheckLine(0, 0, 0, 1, size, currentplayer))
					return true;
			}

			for (int i = 0;i < size; i++)
			{
				if (CheckLine(0, 0,1, 0, size, currentplayer))
					return true;
			}

			if (CheckLine(0, 0, 1, 1, size, currentplayer))
				return true;

			if (CheckLine(0, 0, 1, size-1, size, currentplayer))
				return true;
			return false;

		}
		public bool CheckLine(int startRow,int startCol, int deltarow, int deltacol,int size, char symbol)
		{
			for (int i = 0; i < size; i++) 
			{
				if (GameBoard.Grid[startRow+i*deltarow,startCol+i*deltacol]  != symbol)
					return false;
			}
			return true;

		}
	}
	
}
