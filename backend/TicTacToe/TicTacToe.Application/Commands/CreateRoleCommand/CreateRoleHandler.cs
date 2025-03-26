using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.CreateRoleCommand;

public class CreateRoleHandler(IManagementApiClient apiClient) : IRequestHandler<CreateRoleCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleCreate = new RoleCreateRequest
        {
            Description = request.Description,
            Name = request.Name
        };

        await apiClient.Roles.CreateAsync(roleCreate, cancellationToken);
        return Result.Success;
    }
}