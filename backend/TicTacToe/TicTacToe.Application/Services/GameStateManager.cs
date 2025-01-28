using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class GameStateManager : IGameStateManager
{
    private readonly Dictionary<Guid, GameStateDto> _games = new();

    public void SaveGame(Guid gameId, GameStateDto gameState)
    {
        _games[gameId] = gameState;
    }

    public GameStateDto? GetGame(Guid gameId)
    {
        return _games.GetValueOrDefault(gameId);
    }
}