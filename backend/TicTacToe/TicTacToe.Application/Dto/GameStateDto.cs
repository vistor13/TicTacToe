using TicTacToe.Core.Models;

namespace TicTacToe.Application.Dto;

public sealed record GameStateDto(GameModes GameModes, PlayerTurn CurrentPlayer, GameState State, char[,] Grid);