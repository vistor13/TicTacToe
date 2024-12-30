using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services;

public class MiniMaxAi(IGameProcessor gameProcessor) : IMiniMaxAi
{
    private const int WinScore = 10;
    private const int DrawScore = 0;

    public MoveParameters FindBestMove()
    {
        var player = gameProcessor.CurrentTurn;
        var opponent = player == PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
        var bestScore = int.MinValue;
        (int row, int col) bestMove = (-1, -1);

        var availableCells = gameProcessor.GetBoard().GetAvailableCells();

        foreach (var (row, col) in availableCells)
        {
            var clonedProcessor = gameProcessor.Clone();

            var moveParameters = new MoveParameters(row, col, player);

            var result = clonedProcessor.MakeMove(moveParameters);

            if (result.IsError) continue;

            var moveScore = MiniMax(clonedProcessor.GetBoard(), false, player, opponent);

            if (moveScore > bestScore)
            {
                bestScore = moveScore;
                bestMove = (row, col);
            }
        }

        return new MoveParameters(bestMove.row, bestMove.col, player);
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
            var clonedBoard = board.Clone();
            clonedBoard.MakeMove(new MoveParameters(row, col, isMaximizing ? player : opponent));

            var currentScore = MiniMax(clonedBoard, !isMaximizing, player, opponent);
            bestScore = isMaximizing
                ? Math.Max(bestScore, currentScore)
                : Math.Min(bestScore, currentScore);
        }

        return bestScore;
    }
}