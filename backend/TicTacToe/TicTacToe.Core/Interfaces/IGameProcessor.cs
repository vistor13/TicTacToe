using ErrorOr;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Core.Interfaces;

public interface IGameProcessor
{
    ErrorOr<Success> MakeMove(MoveParameters moveParameters);
    void InitializeGame(bool twoPlayerGame = true);
    Board GetBoard();
    GameProcessor Clone();
    ErrorOr<Success> AiMakeMove(out MoveParameters moveParameters);
    void Reset();
}