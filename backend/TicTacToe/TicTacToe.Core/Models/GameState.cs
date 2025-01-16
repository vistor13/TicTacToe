using System.Text.Json.Serialization;

namespace TicTacToe.Core.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GameState
{
    NotStarted,
    Ongoing,
    Draw,
    Win
}