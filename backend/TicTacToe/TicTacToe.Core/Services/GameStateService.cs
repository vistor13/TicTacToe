using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services;

public class GameStateService : IGameStateService
{
    public GameState State { get; private set; } = GameState.NotStarted;
    public PlayerTurn CurrentTurn { get; private set; }
    public GameModes GameMode { get; private set; }

    public void SetState(GameState state)
    {
        State = state;
    }

    public void SetCurrentTurn(PlayerTurn turn)
    {
        CurrentTurn = turn;
    }

    public void SetGameMode(GameModes mode)
    {
        GameMode = mode;
    }

    public void Reset()
    {
        State = GameState.NotStarted;
        CurrentTurn = PlayerTurn.X;
        GameMode = GameModes.NotDefined;
    }
}