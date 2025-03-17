using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using ErrorOr;
using MediatR;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands.AssignRolesCommand;

public class AssignUserToRolesHandler(IAuthService authService, IManagementApiClient apiClient)
    : IRequestHandler<AssignUserToRolesCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AssignUserToRolesCommand request, CancellationToken cancellationToken)
    {
        if (request.Roles is null) throw new ArgumentNullException(nameof(request.Roles));

        var userRoles = await authService.GetUserRolesAsync(apiClient, request.UserId, cancellationToken);

        var newRoles = request.Roles.Where(role => !userRoles.Contains(role)).ToList();

        var allRoles = await authService.GetAllRolesAsync(apiClient);

        var roleIds = authService.GetRoleIds(newRoles, allRoles);

        if (roleIds.Length == 0) return Error.Validation("Roles.Empty", "No valid roles found to assign.");

        await apiClient.Users.AssignRolesAsync(request.UserId, new AssignRolesRequest { Roles = roleIds });

        return Result.Success;
    }
}