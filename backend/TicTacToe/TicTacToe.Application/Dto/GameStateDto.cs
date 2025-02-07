namespace TicTacToe.Application.Dto;

public sealed record GameStateDto(
    string GameModes,
    string CurrentPlayer,
    string GameState,
    char[,] Grid,
    bool IsRunning,
    bool ShouldAiMove);