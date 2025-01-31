using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class GameStateManager : IGameStateManager
{
    private readonly Dictionary<Guid, GameStateModel> _games = new();

    public void SaveGame(Guid gameId, GameStateModel gameState)
    {
        _games[gameId] = gameState;
    }

    public GameStateModel? GetGame(Guid gameId)
    {
        return _games.GetValueOrDefault(gameId);
    }
}