using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Interfaces;

public interface IGameStateManager
{
    void SaveGame(Guid gameId, GameStateModel gameState);
    GameStateModel? GetGame(Guid gameId);
}