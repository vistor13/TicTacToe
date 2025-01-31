using TicTacToe.Core.Models;

namespace TicTacToe.Application.Dto;

public sealed record GameStateModel(GameModes GameModes, PlayerTurn CurrentPlayer, GameState State, char[,] Grid);