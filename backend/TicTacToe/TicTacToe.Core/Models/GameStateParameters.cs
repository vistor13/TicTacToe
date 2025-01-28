namespace TicTacToe.Core.Models;

public record GameStateParameters(GameState State, char[,] Grid, PlayerTurn PlayerTurn);