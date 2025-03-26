using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.AssignRolesCommand;

public record AssignUserToRolesCommand(string UserId, List<string> Roles) : IRequest<ErrorOr<Success>>;