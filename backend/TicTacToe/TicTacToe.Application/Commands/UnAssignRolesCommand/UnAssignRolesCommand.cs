using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.UnAssignRolesCommand;

public sealed record UnAssignRolesCommand(string Auth0UserId, List<string> Roles) : IRequest<ErrorOr<Success>>;