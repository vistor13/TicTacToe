namespace TicTacToe.Api.Contracts.Requests;

public sealed record UnAssignRolesRequest(string Auth0UserId, List<string> Roles)
    : AssignRoleRequest(Auth0UserId, Roles);