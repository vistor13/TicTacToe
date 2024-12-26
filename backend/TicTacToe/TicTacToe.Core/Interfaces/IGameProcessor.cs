using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IGameProcessor
{
    GameState State { get; }
    PlayerTurn CurrentTurn { get; }
    bool MakeMove(MoveParameters moveParameters);
    void InitializeGame();
    Board GetBoard();
}