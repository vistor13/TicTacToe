using TicTacToe.Core.Models;

namespace TicTacToe.Tests;

public class BoardTests
{
    private const int BoardSize = 3;
    
    private const char EmptyCell = ' ';
    
    private const int RowDimension = 0;

    private const int ColumnDimension = 1;
    
    [Fact]
    public void Board_HasCorrectDimensions_AfterInitialization()
    {
        // Arrange
        var board = new Board();
        
        var grid = board.Grid;

        // Assert
        Assert.Equal(BoardSize, grid.GetLength(RowDimension));
        Assert.Equal(BoardSize, grid.GetLength(ColumnDimension));
    }

    [Fact]
    public void Board_AllCellsAreEmpty_AfterInitialization()
    {
        // Arrange
        var board = new Board();
        
        var grid = board.Grid;

        // Assert
        for (var i = 0; i < BoardSize; i++)
        {
            for (var j = 0; j < BoardSize; j++)
            {
                Assert.Equal(EmptyCell, grid[i, j]);
            }
        }
    }
}