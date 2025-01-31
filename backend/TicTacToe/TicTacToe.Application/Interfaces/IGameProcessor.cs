using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Interfaces;

public interface IGameProcessor
{
    GameModes GameMode { get; }
    bool IsRunning { get; }
    ErrorOr<GameStateDto> MakeMove(MoveParameters moveParameters);
    void InitializeGame(bool twoPlayerGame = true);
    ErrorOr<GameStateDto> AiMakeMove(out MoveParameters moveParameters);
    void Reset();
    GameStateDto GetGameState();
    void LoadGameState(GameStateModel state);
    GameResultDto GetGameResult();
}