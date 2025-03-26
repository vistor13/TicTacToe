namespace TicTacToe.Infrastructure.Auth;

public record Auth0Options
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string Domain { get; init; }
    public required string Authority { get; init; }
    public required string Audience { get; init; }
}