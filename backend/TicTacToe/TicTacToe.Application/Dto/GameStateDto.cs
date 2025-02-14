using TicTacToe.Infrastructure.Entities;

public sealed record GameStateDto(
    string GameModes,
    string CurrentPlayer,
    string GameState,
    char[,] Grid,
    bool IsRunning,
    bool ShouldAiMove)
{
    public static GameStateDto ToDto(GameEntity gameEntity)
    {
        var grid = new char[3, 3];

        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            grid[i, j] = ' ';

        foreach (var move in gameEntity.Moves)
            if (move.Row is >= 0 and < 3 && move.Col is >= 0 and < 3)
                grid[move.Row, move.Col] = move.MoveSymbol;

        return new GameStateDto(
            gameEntity.Mode.ToString(),
            gameEntity.CurrentPlayer.ToString(),
            gameEntity.GameState.ToString(),
            grid,
            gameEntity.IsRunning,
            gameEntity.ShouldAiMove
        );
    }
}