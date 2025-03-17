using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using ErrorOr;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class AuthService : IAuthService
{
    public async Task<List<string>> GetUserRolesAsync(IManagementApiClient apiClient, string userId,
        CancellationToken cancellationToken)
    {
        var assignRole = await apiClient.Users.GetRolesAsync(userId, cancellationToken: cancellationToken);
        return assignRole.Select(r => r.Name).ToList();
    }

    public void ValidateRoles(IEnumerable<string>? roles)
    {
        if (roles is null) throw new ArgumentNullException(nameof(roles));
    }

    public ErrorOr<Success>? ValidateRoleIds(string[] roleIds)
    {
        if (roleIds.Length == 0) return Error.Validation("Roles.Empty", "No valid roles found to assign.");

        return null;
    }

    public async Task<IPagedList<Role>> GetAllRolesAsync(IManagementApiClient apiClient)
    {
        return await apiClient.Roles.GetAllAsync(new GetRolesRequest());
    }

    public string?[] GetRoleIds(List<string> newRoles, IPagedList<Role> allRoles)
    {
        return newRoles
            .Select(role =>
                allRoles.FirstOrDefault(apiRole => apiRole.Name.Equals(role, StringComparison.OrdinalIgnoreCase))?.Id)
            .Where(id => id != null)
            .ToArray();
    }
}