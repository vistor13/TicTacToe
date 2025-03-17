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
        authService.ValidateRoles(request.Roles);

        var userRoles = await authService.GetUserRolesAsync(apiClient, request.UserId, cancellationToken);

        var newRoles = request.Roles.Where(role => !userRoles.Contains(role)).ToList();

        var allRoles = await authService.GetAllRolesAsync(apiClient);

        var roleIds = authService.GetRoleIds(newRoles, allRoles);

        var validateRoleIds = authService.ValidateRoleIds(roleIds);
        if (validateRoleIds is not null) return validateRoleIds.Value.FirstError;

        await apiClient.Users.AssignRolesAsync(request.UserId, new AssignRolesRequest { Roles = roleIds });

        return Result.Success;
    }
}