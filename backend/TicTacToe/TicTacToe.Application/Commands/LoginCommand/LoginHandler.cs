using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using MediatR;
using Microsoft.Extensions.Options;
using TicTacToe.Infrastructure.Auth;

namespace TicTacToe.Application.Commands.LoginCommand;

public class LoginHandler(IOptions<Auth0Options> options) : IRequestHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var auth0Info = options.Value;
        var auth0Client = new AuthenticationApiClient(auth0Info.Domain);
        return await auth0Client.GetTokenAsync(new ResourceOwnerTokenRequest
        {
            Username = request.Login,
            Password = request.Password,
            ClientId = auth0Info.ClientId,
            Audience = auth0Info.Audience,
            ClientSecret = auth0Info.ClientSecret,
            Scope = "openid"
        });
    }
}