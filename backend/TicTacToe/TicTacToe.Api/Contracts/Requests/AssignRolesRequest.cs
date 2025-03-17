namespace TicTacToe.Api.Contracts.Requests;

public sealed record AssignRoleRequest(string Auth0UserId, List<string> Roles);