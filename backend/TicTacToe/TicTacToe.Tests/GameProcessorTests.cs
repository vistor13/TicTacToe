using TicTacToe.Core.Models;

namespace TicTacToe.Tests
{
	public class GameProcessorTests
	{
		[Fact]
		public void GameProcessor_MakeMove_ShouldUpdateBoardAndSwitchPlayer_WhenMoveIsValid()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			var result = game.MakeMove(0, 0, PlayerTurn.X);

			// Assert
			Assert.True(result);
			Assert.Equal('X', game.GameBoard.Grid[0, 0]);
			Assert.Equal(PlayerTurn.Y, game.CurrentTurn);
		}

		[Fact]
		public void GameProcessor_MakeMove_ShouldNotSwitchPlayer_WhenMoveIsInvalid()
		{
			// Arrange
			var game = new GameProcessor();
			game.MakeMove(0, 0, PlayerTurn.X);

			// Act
			var result = game.MakeMove(0, 0, PlayerTurn.Y);

			// Assert
			Assert.False(result);
			Assert.Equal(PlayerTurn.Y, game.CurrentTurn);
		}

		[Fact]
		public void GameProcessor_MakeMove_ShouldReturnFalse_WhenMoveIsOutOfBounds()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			var result = game.MakeMove(3, 5, PlayerTurn.X);

			// Assert
			Assert.False(result);
			Assert.Equal(PlayerTurn.X, game.CurrentTurn);
		}

		[Fact]
		public void GameProcessor_MakeMove_ShouldValidateTurnOrder_WhenMoveIsMadeByIncorrectPlayer()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			var result = game.MakeMove(0, 0, PlayerTurn.Y);

			// Assert
			Assert.False(result);
			Assert.Equal(PlayerTurn.X, game.CurrentTurn);
		}


		[Fact]
		public void GameProcessor_ShouldDeclarePlayerOneWin_WhenHorizontalLineIsFormed()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			game.MakeMove(0, 0, PlayerTurn.X);
			game.MakeMove(1, 0, PlayerTurn.Y);
			game.MakeMove(0, 1, PlayerTurn.X);
			game.MakeMove(1, 1, PlayerTurn.Y);
			game.MakeMove(0, 2, PlayerTurn.X);

			// Assert
			Assert.Equal(GameState.PlayerOneWin, game.State);
		}

		[Fact]
		public void GameProcessor_ShouldDeclarePlayerOneWin_WhenVerticalLineIsFormed()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			game.MakeMove(0, 0, PlayerTurn.X);
			game.MakeMove(0, 1, PlayerTurn.Y);
			game.MakeMove(1, 0, PlayerTurn.X);
			game.MakeMove(1, 1, PlayerTurn.Y);
			game.MakeMove(2, 0, PlayerTurn.X);

			// Assert
			Assert.Equal(GameState.PlayerOneWin, game.State);
		}

		[Fact]
		public void GameProcessor_ShouldDeclarePlayerOneWin_WhenDiagonalLineIsFormed()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			game.MakeMove(0, 0, PlayerTurn.X);
			game.MakeMove(0, 1, PlayerTurn.Y);
			game.MakeMove(1, 1, PlayerTurn.X);
			game.MakeMove(1, 2, PlayerTurn.Y);
			game.MakeMove(2, 2, PlayerTurn.X);

			// Assert
			Assert.Equal(GameState.PlayerOneWin, game.State);
		}
		[Fact]
		public void GameProcessor_ShouldDeclareDraw_WhenAllCellsAreFilledWithoutWinner()
		{
			// Arrange
			var game = new GameProcessor();

			game.MakeMove(0, 0, PlayerTurn.X);
			game.MakeMove(0, 1, PlayerTurn.Y);
			game.MakeMove(0, 2, PlayerTurn.X);
			game.MakeMove(1, 1, PlayerTurn.Y);
			game.MakeMove(1, 0, PlayerTurn.X);
			game.MakeMove(1, 2, PlayerTurn.Y);
			game.MakeMove(2, 1, PlayerTurn.X);
			game.MakeMove(2, 0, PlayerTurn.Y);
			game.MakeMove(2, 2, PlayerTurn.X);

			// Assert
			Assert.Equal(GameState.Draw, game.State);

			var moveAfterDraw = game.MakeMove(0, 0, PlayerTurn.Y);
			Assert.False(moveAfterDraw);
		}
	}
}