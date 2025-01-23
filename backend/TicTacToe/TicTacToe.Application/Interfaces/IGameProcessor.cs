using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Interfaces;

public interface IGameProcessor
{
    GameModes GameMode { get; }
    bool IsRunning { get; }
    ErrorOr<Success> MakeMove(MoveParameters moveParameters);
    void InitializeGame(bool twoPlayerGame = true);
    Board GetBoard();
    ErrorOr<Success> AiMakeMove(out MoveParameters moveParameters);
    void Reset();
    GameStateDto GetGameState();
    void LoadGameState(GameStateParameters state);
}