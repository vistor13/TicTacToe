namespace TicTacToe.Core.Models
{
	public enum GameState
	{
		Ongoing,
		Draw,
		Win
	}
	public enum PlayerTurn
	{
		X,
		О
	}
	public class GameProcessor
	{
		public Board GameBoard { get; set; }
		public GameState State { get; set; }
		public PlayerTurn CurrentTurn { get; set; }

		public GameProcessor()
		{
			GameBoard = new Board();
			State = GameState.Ongoing;
			CurrentTurn = PlayerTurn.X;
		}
		public bool MakeMove(MoveParameters moveParameters)
		{
			if (State != GameState.Ongoing || CurrentTurn != moveParameters.PlayerTurn)
				return false;

			if (!GameBoard.CanMakeMove(moveParameters))
				return false;

			GameBoard.MakeMove(moveParameters);

			if (CheckWin())
			{
				State = GameState.Win;
				return false;
			}

			if (CheckDraw())
			{
				State = GameState.Draw;
				return false;
			}

			SwitchTurn();

			return true;
		}
		private void SwitchTurn()
		{
			CurrentTurn = CurrentTurn is PlayerTurn.X
				? PlayerTurn.О
				: PlayerTurn.X;
		}
		private bool CheckWin()
		{
			return CheckLines(uniqueCells => uniqueCells.Count == 1 && !uniqueCells.Contains(' '));
		}
		private bool CheckDraw()
		{
			if (GameBoard.IsBoardFull())
				return true;

			return !CheckLines(uniqueCells => uniqueCells.Count == 2 && uniqueCells.Contains(' '));
		}
		private bool CheckLines(Predicate<HashSet<char>> condition)
		{
			var lines = GameBoard.GetAllLines();

			foreach (var line in lines)
			{
				var uniqueCells = new HashSet<char>(line);
				if (condition(uniqueCells))
					return true;
			}
			return false;
		}

	}
}
