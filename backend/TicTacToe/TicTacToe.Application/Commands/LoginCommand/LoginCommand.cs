using Auth0.AuthenticationApi.Models;
using MediatR;

namespace TicTacToe.Application.Commands.LoginCommand;

public sealed record LoginCommand(string Login, string Password) : IRequest<AccessTokenResponse>;