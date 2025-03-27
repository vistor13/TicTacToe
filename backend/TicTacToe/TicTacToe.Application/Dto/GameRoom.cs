namespace TicTacToe.Application.Dto;

public class GameRoom
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string PlayerOne { get; set; }
    public string? PlayerTwo { get; set; }
    public string Status { get; set; } = "Waiting";
    public GameStateModel GameState { get; set; }

    public Dictionary<string, string> PlayerSymbols { get; } = new();

    public void AssignSymbols()
    {
        if (!string.IsNullOrEmpty(PlayerOne))
            PlayerSymbols[PlayerOne] = "X";

        if (!string.IsNullOrEmpty(PlayerTwo))
            PlayerSymbols[PlayerTwo] = "O";
    }
}