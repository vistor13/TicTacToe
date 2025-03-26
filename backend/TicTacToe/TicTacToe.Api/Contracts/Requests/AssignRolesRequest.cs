namespace TicTacToe.Api.Contracts.Requests;

public record AssignRoleRequest(string Auth0UserId, List<string> Roles);