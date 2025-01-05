using ErrorOr;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IGameProcessor
{
    GameModes GameMode { get; }

    bool IsRunning { get; set; }
    ErrorOr<Success> MakeMove(MoveParameters moveParameters);
    void InitializeGame(bool twoPlayerGame = true);
    Board GetBoard();
    ErrorOr<Success> AiMakeMove(out MoveParameters moveParameters);
    void Reset();
}