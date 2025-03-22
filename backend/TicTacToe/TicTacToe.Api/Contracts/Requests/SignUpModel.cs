namespace TicTacToe.Api.Contracts.Requests;

/// <summary>
/// </summary>
public sealed record SignUpModel(string Email, string Password, string FirstName, string LastName);