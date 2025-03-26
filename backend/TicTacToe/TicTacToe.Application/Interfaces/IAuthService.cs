using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using ErrorOr;

namespace TicTacToe.Application.Interfaces;

public interface IAuthService
{
    Task<List<string>> GetUserRolesAsync(IManagementApiClient apiClient, string userId,
        CancellationToken cancellationToken);

    Task<IPagedList<Role>> GetAllRolesAsync(IManagementApiClient apiClient);
    string?[] GetRoleIds(List<string> newRoles, IPagedList<Role> allRoles);
    void ValidateRoles(IEnumerable<string>? roles);
    ErrorOr<Success>? ValidateRoleIds(string[] roleIds);
}