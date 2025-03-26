using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using ErrorOr;
using MediatR;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands.UnAssignRolesCommand;

public class UnAssignRolesHandler(IAuthService service, IManagementApiClient apiClient)
    : IRequestHandler<UnAssignRolesCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UnAssignRolesCommand request, CancellationToken cancellationToken)
    {
        service.ValidateRoles(request.Roles);

        var userRoles = await service.GetUserRolesAsync(apiClient, request.Auth0UserId, cancellationToken);

        var deleteRoles = request.Roles.Where(x => userRoles.Contains(x)).ToList();

        var allRoles = await service.GetAllRolesAsync(apiClient);

        var roleIds = service.GetRoleIds(deleteRoles, allRoles);

        var validateRoleIds = service.ValidateRoleIds(roleIds);
        if (validateRoleIds is not null) return validateRoleIds.Value.FirstError;

        await apiClient.Users.RemoveRolesAsync(request.Auth0UserId, new AssignRolesRequest
        {
            Roles = roleIds
        });
        return Result.Success;
    }
}