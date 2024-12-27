using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Tests
{
    public class GameProcessorTests
    {
        [Fact]
        public void GameProcessor_MakeMove_ShouldUpdateBoardAndSwitchPlayer_WhenMoveIsValid()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();
            var move = new MoveParameters(0, 0, PlayerTurn.X);

            // Act
            var result = game.MakeMove(move);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal('X', game.GameBoard.Grid[0, 0]);
            Assert.Equal(PlayerTurn.О, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_MakeMove_ShouldNotSwitchPlayer_WhenMoveIsInvalid()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();
            var moveX = new MoveParameters(0, 0, PlayerTurn.X);
            var moveY = new MoveParameters(0, 0, PlayerTurn.О);
            game.MakeMove(moveX);

            // Act
            var result = game.MakeMove(moveY);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(PlayerTurn.О, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_MakeMove_ShouldReturnFalse_WhenMoveIsOutOfBounds()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();
            var move = new MoveParameters(3, 5, PlayerTurn.X);

            // Act
            var result = game.MakeMove(move);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(PlayerTurn.X, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_MakeMove_ShouldValidateTurnOrder_WhenMoveIsMadeByIncorrectPlayer()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();
            var move = new MoveParameters(0, 0, PlayerTurn.О);

            // Act
            var result = game.MakeMove(move);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(PlayerTurn.X, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_ShouldDeclareWin_WhenHorizontalLineIsFormed()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();

            // Act
            game.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 0, PlayerTurn.О));
            game.MakeMove(new MoveParameters(0, 1, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(0, 2, PlayerTurn.X));

            // Assert
            Assert.Equal(GameState.Win, game.State);
            Assert.Equal(PlayerTurn.X, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_ShouldDeclareWin_WhenVerticalLineIsFormed()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();

            // Act
            game.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(0, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(1, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(2, 0, PlayerTurn.X));

            // Assert
            Assert.Equal(GameState.Win, game.State);
            Assert.Equal(PlayerTurn.X, game.CurrentTurn);
        }


        [Fact]
        public void GameProcessor_ShouldDeclareWin_WhenDiagonalLineIsFormed()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();

            // Act
            game.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(0, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(1, 1, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 2, PlayerTurn.О));
            game.MakeMove(new MoveParameters(2, 2, PlayerTurn.X));

            // Assert
            Assert.Equal(GameState.Win, game.State);
            Assert.Equal(PlayerTurn.X, game.CurrentTurn);
        }

        [Fact]
        public void GameProcessor_ShouldDeclareDraw_WhenAllCellsAreFilledWithoutWinner()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();

            // Act
            game.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(0, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(0, 2, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(1, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 2, PlayerTurn.О));
            game.MakeMove(new MoveParameters(2, 1, PlayerTurn.X));
            game.MakeMove(new MoveParameters(2, 0, PlayerTurn.О));
            game.MakeMove(new MoveParameters(2, 2, PlayerTurn.X));

            // Assert
            Assert.Equal(GameState.Draw, game.State);

            var moveAfterDraw = game.MakeMove(new MoveParameters(0, 0, PlayerTurn.О));
            Assert.False(moveAfterDraw.IsSuccess);
        }

        [Fact]
        public void GameProcessor_ShouldDeclareDraw_WhenNoWinningCombinationsRemain()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();
            game.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 0, PlayerTurn.О));
            game.MakeMove(new MoveParameters(2, 0, PlayerTurn.X));
            game.MakeMove(new MoveParameters(1, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(1, 2, PlayerTurn.X));
            game.MakeMove(new MoveParameters(2, 1, PlayerTurn.О));
            game.MakeMove(new MoveParameters(0, 1, PlayerTurn.X));


            // Act
            game.MakeMove(new MoveParameters(0, 2, PlayerTurn.О));

            // Assert
            Assert.Equal(GameState.Draw, game.State);
            var moveAfterBoardIsFull = game.MakeMove(new MoveParameters(2, 2, PlayerTurn.О));
            Assert.False(moveAfterBoardIsFull.IsSuccess);
        }

        [Fact]
        public void GameProcessor_GameStatusIsOngoing_AfterInitialization()
        {
            // Arrange
            var game = new GameProcessor();
            game.InitializeGame();

            // Act
            var gameStatus = game.State;

            // Assert
            Assert.Equal(GameState.Ongoing, gameStatus);
        }
    }
}