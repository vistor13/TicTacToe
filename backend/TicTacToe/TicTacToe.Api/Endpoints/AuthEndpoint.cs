using System.Text;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Extensions;
using TicTacToe.Application.Commands.AssignRolesCommand;
using TicTacToe.Application.Commands.UnAssignRolesCommand;
using TicTacToe.Infrastructure.Auth;

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
        endpoints.MapPost("roles/assign", AssignRoles);
        endpoints.MapPost("roles/unassign", UnAssignRoles);
    }

    private static async Task<IResult> Login([FromBody] SignInModel req,
        [FromServices] IOptions<Auth0Options> auth0Options)
    {
        var auth0Info = auth0Options.Value;
        var auth0Client = new AuthenticationApiClient(auth0Info.Domain);
        var tokenResponse = await auth0Client.GetTokenAsync(new ResourceOwnerTokenRequest
        {
            Username = req.Login,
            Password = req.Password,
            ClientId = auth0Info.ClientId,
            Audience = auth0Info.Audience,
            ClientSecret = auth0Info.ClientSecret,
            Scope = "openid"
        });

        return Results.Ok(new { access_token = tokenResponse.AccessToken });
    }

    private static async Task<IResult> Register([FromBody] SignUpModel req,
        [FromServices] IOptions<Auth0Options> auth0Options)
    {
        var auth0Info = auth0Options.Value;
        var httpClient = new HttpClient();

        var requestData = new
        {
            client_id = auth0Info.ClientId,
            email = req.Email,
            password = req.Password,
            connection = "Username-Password-Authentication",
            given_name = req.FirstName,
            family_name = req.LastName
        };

        var jsonRequest = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"https://{auth0Info.Domain}/dbconnections/signup", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        return !response.IsSuccessStatusCode
            ? Results.BadRequest(new { Error = responseContent })
            : Results.Ok();
    }

    private static async Task<IResult> CreateRole([FromBody] RoleRequest roleRequest,
        [FromServices] IOptions<Auth0Options> auth0Options, [FromServices] IManagementApiClient auth0Client)
    {
        var roleCreate = new RoleCreateRequest
        {
            Description = roleRequest.Description,
            Name = roleRequest.Name
        };

        await auth0Client.Roles.CreateAsync(roleCreate);
        return Results.Ok();
    }

    private static async Task<IResult> AssignRoles([FromBody] AssignRoleRequest request,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new AssignUserToRolesCommand(request.Auth0UserId, request.Roles));
        return response.ToResult();
    }

    private static async Task<IResult> UnAssignRoles([FromBody] UnAssignRolesRequest request,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new UnAssignRolesCommand(request.Auth0UserId, request.Roles));
        return response.ToResult();
    }
}