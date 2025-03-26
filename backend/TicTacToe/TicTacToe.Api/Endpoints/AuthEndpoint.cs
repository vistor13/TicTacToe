using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Extensions;
using TicTacToe.Application.Commands.AssignRolesCommand;
using TicTacToe.Application.Commands.CreateRoleCommand;
using TicTacToe.Application.Commands.LoginCommand;
using TicTacToe.Application.Commands.RegisterCommand;
using TicTacToe.Application.Commands.UnAssignRolesCommand;

namespace TicTacToe.Api.Endpoints;

/// <summary>
/// </summary>
public static class AuthEndpoint
{
    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/api/auth/").WithTags("Auth");

        endpoints.MapPost("login", Login);
        endpoints.MapPost("register", Register);
        endpoints.MapPost("roles", CreateRole);
        endpoints.MapPost("roles/assign", AssignRoles).RequireAuthorization("Admin");
        endpoints.MapPost("roles/unassign", UnAssignRoles).RequireAuthorization("Admin");
    }

    private static async Task<IResult> Login([FromBody] SignInModel req,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(
            new LoginCommand(req.Login, req.Password));
        return Results.Ok(new { access_token = response.AccessToken });
    }

    private static async Task<IResult> Register([FromBody] SignUpModel req,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(
            new RegisterCommand(req.Email, req.Password, req.FirstName, req.LastName));
        return response.ToResult();
    }

    private static async Task<IResult> CreateRole([FromBody] RoleRequest roleRequest, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(
            new CreateRoleCommand(roleRequest.Name, roleRequest.Description));
        return response.ToResult();
    }

    private static async Task<IResult> AssignRoles([FromBody] AssignRoleRequest request,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(
            new AssignUserToRolesCommand(request.Auth0UserId, request.Roles));
        return response.ToResult();
    }

    private static async Task<IResult> UnAssignRoles([FromBody] UnAssignRolesRequest request,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(
            new UnAssignRolesCommand(request.Auth0UserId, request.Roles));
        return response.ToResult();
    }
}