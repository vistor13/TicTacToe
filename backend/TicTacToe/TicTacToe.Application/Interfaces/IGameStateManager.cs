using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Interfaces;

public interface IGameStateManager
{
    void SaveGame(Guid gameId, GameStateDto gameState);
    GameStateDto? GetGame(Guid gameId);
}