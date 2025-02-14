using TicTacToe.Core.Models;
using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Application.Dto;

public sealed record GameStateModel(
    GameModes Modes,
    GameState State,
    PlayerTurn CurrentPlayer,
    char[,] Grid,
    bool IsRunning,
    bool ShouldAiMove)
{
    public static GameEntity ToEntity(GameStateModel gameStateModel, GameEntity? existingEntity = null)
    {
        var entity = existingEntity ?? new GameEntity();

        entity.GameState = gameStateModel.State;
        entity.Mode = gameStateModel.Modes;
        entity.CurrentPlayer = gameStateModel.CurrentPlayer;
        entity.IsRunning = gameStateModel.IsRunning;
        entity.ShouldAiMove = gameStateModel.ShouldAiMove;
        entity.Moves.Clear();

        for (var row = 0; row < 3; row++)
        for (var col = 0; col < 3; col++)
        {
            var moveSymbol = gameStateModel.Grid[row, col];
            if (moveSymbol != '\0')
                entity.Moves.Add(new MoveEntity
                {
                    Row = row,
                    Col = col,
                    MoveSymbol = moveSymbol
                });
        }

        return entity;
    }

    public static GameStateModel ToModel(GameEntity gameEntity)
    {
        var grid = new char[3, 3];
        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            grid[i, j] = ' ';

        foreach (var move in gameEntity.Moves)
        {
            if (move.Row is >= 0 and < 3 && move.Col is >= 0 and < 3) grid[move.Row, move.Col] = move.MoveSymbol;
        }

        return new GameStateModel(
            gameEntity.Mode,
            gameEntity.GameState,
            gameEntity.CurrentPlayer,
            grid,
            gameEntity.IsRunning,
            gameEntity.ShouldAiMove
        );
    }
}