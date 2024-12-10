using TicTacToe.Core.Models;

namespace TicTacToe.Tests
{
	public class GameProcessorTests
	{
		[Fact]
		public void MakeMove_ValidMove_UpdatesBoardAndSwitchesPlayer()
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
		public void MakeMove_InvalidMove_DoesNotSwitchPlayer()
		{
			// Arrange
			var game = new GameProcessor();

			// Виконуємо перший валідний хід
			game.MakeMove(0, 0, PlayerTurn.X);

			// Act
			var result = game.MakeMove(0, 0, PlayerTurn.Y);

			// Assert
			Assert.False(result);
			Assert.Equal(PlayerTurn.Y, game.CurrentTurn);
		}

		[Fact]
		public void MakeMove_InvalidMove_WentBeyondTheBoundaries()
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
		public void MakeMove_CorrectlyValidatesTurnOrder()
		{
			// Arrange
			var game = new GameProcessor();

			// Act
			var result = game.MakeMove(0, 0, PlayerTurn.Y);

			// Assert
			Assert.False(result);
			Assert.Equal(PlayerTurn.X, game.CurrentTurn);
		}
	}
}