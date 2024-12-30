using ErrorOr;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IGameProcessor
{
    GameState State { get; }
    PlayerTurn CurrentTurn { get; }
    GameModes GameModes { get; }
    ErrorOr<Success> MakeMove(MoveParameters moveParameters);
    void InitializeGame(bool isGameWithPlayer = true);
    Board GetBoard();
    GameProcessor Clone();
}