namespace TicTacToe.Api.Contracts.Requests;

/// <summary>
/// </summary>
public record SignUpModel
{
    /// <summary>
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// </summary>
    public required string LastName { get; init; }
}