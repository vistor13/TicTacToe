using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.CreateRoleCommand;

public sealed record CreateRoleCommand(string Name, string Description) : IRequest<ErrorOr<Success>>;