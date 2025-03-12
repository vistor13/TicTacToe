using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TicTacToe.Api.Contracts.Requests;
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
        var auth0AuthClient = new AuthenticationApiClient(auth0Info.Domain);

        var tokenResponse = await auth0AuthClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            ClientId = auth0Info.ClientId,
            ClientSecret = auth0Info.ClientSecret,
            Audience = auth0Info.Audience
        });

        var auth0Client = new ManagementApiClient(tokenResponse.AccessToken, new Uri(auth0Info.Audience));

        var newUser = new UserCreateRequest
        {
            Email = req.Email,
            Password = req.Password,
            Connection = "Username-Password-Authentication",
            FirstName = req.FirstName,
            LastName = req.LastName
        };

        var createdUser = await auth0Client.Users.CreateAsync(newUser);
        return Results.Ok(createdUser.UserId);
    }
}