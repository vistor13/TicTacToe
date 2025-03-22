using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using ErrorOr;
using MediatR;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands.RegisterCommand;

public class RegisterHandler(IManagementApiClient apiClient, IAuthService authService)
    : IRequestHandler<RegisterCommand, ErrorOr<Success>>
{
    private const string Defaultrole = "player";

    public async Task<ErrorOr<Success>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new UserCreateRequest
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Connection = "Username-Password-Authentication"
        };

        var createdUser = await apiClient.Users.CreateAsync(user, cancellationToken);
        if (createdUser is null)
            return Error.Unexpected(
                "UserCreationFailed",
                Messages.Error.UserCreationFailed);

        var allRoles = await authService.GetAllRolesAsync(apiClient);
        var roleIds = authService.GetRoleIds([Defaultrole], allRoles);

        await apiClient.Users.AssignRolesAsync(createdUser.UserId, new AssignRolesRequest
        {
            Roles = roleIds
        }, cancellationToken);

        return Result.Success;
    }
}