using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IGameStateService
{
    GameState State { get; }
    PlayerTurn CurrentTurn { get; }
    GameModes GameMode { get; }
    void Reset();
    void SetState(GameState state);
    void SetGameMode(GameModes mode);
    void SetCurrentTurn(PlayerTurn turn);
}