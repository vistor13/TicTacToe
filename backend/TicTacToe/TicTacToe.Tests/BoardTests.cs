using TicTacToe.Core.Models;

namespace TicTacToe.Tests;

public class BoardTests
{
    private const int RowDimension = 0;

    private const int ColumnDimension = 1;

    [Fact]
    public void InitializeGameState_ShouldSetGameStateToOngoing()
    {
        // Arrange
        var board = new Board();

        // Act
        board.InitializeGameState();

        // Assert
        Assert.Equal(GameState.Ongoing, board.State);
    }

    [Fact]
    public void MakeMove_ShouldPlaceSymbolAndSwitchTurn()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        var move = new MoveParameters(0, 0, PlayerTurn.X);

        // Act
        var result = board.MakeMove(move);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal('X', board.GetCell(0, 0));
        Assert.Equal(PlayerTurn.О, board.CurrentTurn);
    }

    [Fact]
    public void MakeMove_ShouldReturnError_WhenCellIsNotEmpty()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        var firstMove = new MoveParameters(0, 0, PlayerTurn.X);
        board.MakeMove(firstMove);

        var secondMove = new MoveParameters(0, 0, PlayerTurn.X);

        // Act
        var result = board.MakeMove(secondMove);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "CellOccupied");
    }

    [Fact]
    public void GetGameStatus_ShouldReturnWin_WhenPlayerHasWinningLine()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        board.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
        board.MakeMove(new MoveParameters(0, 1, PlayerTurn.X));
        board.MakeMove(new MoveParameters(0, 2, PlayerTurn.X));

        // Act
        var status = board.GetGameStatus();

        // Assert
        Assert.Equal(GameState.Win, status);
    }

    [Fact]
    public void GetGameStatus_ShouldReturnDraw_WhenBoardIsFullAndNoWinner()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        var moves = new[]
        {
            new MoveParameters(0, 0, PlayerTurn.X),
            new MoveParameters(0, 1, PlayerTurn.О),
            new MoveParameters(0, 2, PlayerTurn.X),
            new MoveParameters(1, 0, PlayerTurn.О),
            new MoveParameters(1, 1, PlayerTurn.X),
            new MoveParameters(1, 2, PlayerTurn.О),
            new MoveParameters(2, 0, PlayerTurn.О),
            new MoveParameters(2, 1, PlayerTurn.X),
            new MoveParameters(2, 2, PlayerTurn.О)
        };


        foreach (var move in moves) board.MakeMove(move);

        // Act
        var status = board.GetGameStatus();

        // Assert
        Assert.Equal(GameState.Draw, status);
    }

    [Fact]
    public void GetAvailableCells_ShouldReturnAllEmptyCells()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        board.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));

        // Act
        var availableCells = board.GetAvailableCells();

        // Assert
        Assert.DoesNotContain((0, 0), availableCells);
        Assert.Equal(Board.BoardSize * Board.BoardSize - 1, availableCells.Count);
    }

    [Fact]
    public void MakeMove_ShouldReturnError_WhenMoveIsOutOfBounds()
    {
        // Arrange
        var board = new Board();
        board.InitializeGameState();

        var move = new MoveParameters(-2, 0, PlayerTurn.X);

        // Act
        var result = board.MakeMove(move);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "OutOfBounds");
    }

    [Fact]
    public void Board_HasCorrectDimensions_AfterInitialization()
    {
        // Arrange
        var board = new Board();

        var grid = board.Grid;

        // Assert
        Assert.Equal(Board.BoardSize, grid.GetLength(RowDimension));
        Assert.Equal(Board.BoardSize, grid.GetLength(ColumnDimension));
    }

    [Fact]
    public void Board_AllCellsAreEmpty_AfterInitialization()
    {
        // Arrange
        var board = new Board();

        var grid = board.Grid;

        // Assert
        for (var i = 0; i < Board.BoardSize; i++)
        {
            for (var j = 0; j < Board.BoardSize; j++)
            {
                Assert.Equal(Board.EmptyCell, grid[i, j]);
            }
        }
    }
}