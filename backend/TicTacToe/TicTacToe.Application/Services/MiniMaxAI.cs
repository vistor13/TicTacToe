using Force.DeepCloner;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Services;

public class MiniMaxAi : IMiniMaxAi
{
    private const int WinScore = 10;
    private const int DrawScore = 0;

    public MoveParametersDto FindBestMove(Board board)
    {
        var player = board.CurrentTurn;
        var opponent = player == PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
        var bestScore = int.MinValue;
        (int row, int col) bestMove = (-1, -1);

        var availableCells = board.GetAvailableCells();

        foreach (var (row, col) in availableCells)
        {
            var clonedBoard = board.DeepClone();

            var moveParameters = new MoveParameters(row, col, player);

            var result = clonedBoard.MakeMove(moveParameters);

            if (result.IsError) continue;

            var moveScore = MiniMax(clonedBoard, false, player, opponent);

            if (moveScore <= bestScore) continue;
            bestScore = moveScore;
            bestMove = (row, col);
        }

        return new MoveParametersDto(bestMove.row, bestMove.col, player.ToString());
    }

    private int MiniMax(Board board, bool isMaximizing, PlayerTurn player, PlayerTurn opponent)
    {
        var gameState = board.GetGameStatus();

        if (gameState == GameState.Win)
            return isMaximizing ? -WinScore : WinScore;

        if (gameState == GameState.Draw)
            return DrawScore;

        var availableCells = board.GetAvailableCells();
        var bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        foreach (var (row, col) in availableCells)
        {
            var clonedBoard = board.DeepClone();
            clonedBoard.MakeMove(new MoveParameters(row, col, isMaximizing ? player : opponent));

            var currentScore = MiniMax(clonedBoard, !isMaximizing, player, opponent);
            bestScore = isMaximizing
                ? Math.Max(bestScore, currentScore)
                : Math.Min(bestScore, currentScore);
        }

        return bestScore;
    }
}