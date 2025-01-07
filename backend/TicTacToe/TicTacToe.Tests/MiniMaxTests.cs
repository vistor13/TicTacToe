using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Tests;

public class MiniMaxAiTests
{
    private readonly MiniMaxAi _miniMaxAi = new();

    [Fact]
    public void FindBestMove_ShouldReturnWinningMove_WhenWinningMoveAvailable()
    {
        // Arrange
        var board = new Board();
        board.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
        board.MakeMove(new MoveParameters(1, 0, PlayerTurn.О));
        board.MakeMove(new MoveParameters(0, 1, PlayerTurn.X));
        board.MakeMove(new MoveParameters(1, 1, PlayerTurn.О));
        board.MakeMove(new MoveParameters(2, 2, PlayerTurn.X));

        // Act
        var bestMove = _miniMaxAi.FindBestMove(board);

        // Assert
        Assert.Equal(0, bestMove.Row);
        Assert.Equal(2, bestMove.Col);
    }

    [Fact]
    public void FindBestMove_ShouldBlockOpponentWin_WhenBlockNeeded()
    {
        // Arrange
        var board = new Board();
        board.MakeMove(new MoveParameters(0, 0, PlayerTurn.X));
        board.MakeMove(new MoveParameters(2, 2, PlayerTurn.О));
        board.MakeMove(new MoveParameters(0, 1, PlayerTurn.X));

        // Act
        var bestMove = _miniMaxAi.FindBestMove(board);

        // Assert
        Assert.Equal(0, bestMove.Row);
        Assert.Equal(2, bestMove.Col);
    }


    [Fact]
    public void FindBestMove_ShouldReturnAnyMove_WhenBoardIsEmpty()
    {
        // Arrange
        var board = new Board();

        // Act
        var bestMove = _miniMaxAi.FindBestMove(board);

        // Assert
        Assert.True(bestMove.Row is >= 0 and < Board.BoardSize);
        Assert.True(bestMove.Col is >= 0 and < Board.BoardSize);
        Assert.Equal(PlayerTurn.X, bestMove.PlayerTurn);
    }
}