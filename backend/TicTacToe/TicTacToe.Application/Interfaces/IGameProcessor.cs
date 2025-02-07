using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Interfaces;

public interface IGameProcessor
{
    GameModes GameMode { get; }
    bool IsRunning { get; }
    bool ShouldAiMove { get; }
    ErrorOr<GameStateDto> MakeMove(MoveParametersDto moveParameters);
    void InitializeGame(bool twoPlayerGame = true);
    ErrorOr<GameStateDto> AiMakeMove(out MoveParametersDto moveParameters);
    void Reset();
    GameStateDto GetGameState();
    void LoadGameState(GameStateModel state);
    GameResultDto GetGameResult();
}