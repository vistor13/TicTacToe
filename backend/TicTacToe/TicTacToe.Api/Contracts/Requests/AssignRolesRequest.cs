namespace TicTacToe.Api.Contracts.Requests;

public record AssignRoleRequest
{
    public string Auth0UserId { get; init; }
    public List<string> Roles { get; init; }
}