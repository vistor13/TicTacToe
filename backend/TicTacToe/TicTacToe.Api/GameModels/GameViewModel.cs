using TicTacToe.Core.Models;

namespace TicTacToe.Api.GameModels;

public record GameViewModel
{
    public Guid Id { get; init; }
    public GameModes GameMode { get; init; }
}