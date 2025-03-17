using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;

namespace TicTacToe.Application.Interfaces;

public interface IAuthService
{
    Task<List<string>> GetUserRolesAsync(IManagementApiClient apiClient, string userId,
        CancellationToken cancellationToken);

    Task<IPagedList<Role>> GetAllRolesAsync(IManagementApiClient apiClient);
    string?[] GetRoleIds(List<string> newRoles, IPagedList<Role> allRoles);
}