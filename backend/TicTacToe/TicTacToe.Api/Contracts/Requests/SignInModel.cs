namespace TicTacToe.Api.Contracts.Requests;

/// <summary>
/// </summary>
public class SignInModel
{
    /// <summary>
    /// </summary>
    public required string Login { get; init; }

    /// <summary>
    /// </summary>
    public required string Password { get; init; }
}