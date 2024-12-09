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

	public class Game
	{
		public Board GameBoard { get; set; }
		public GameState State { get; set; }
		public PlayerTurn CurrentTurn{ get; set; }
		public Game() 
		{
			GameBoard= new Board();
			State = GameState.Ongoing;
			CurrentTurn = PlayerTurn.X;
		}

		
	}
	
}
