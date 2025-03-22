using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.RegisterCommand;

public sealed record RegisterCommand(string Email, string Password, string FirstName, string LastName)
    : IRequest<ErrorOr<Success>>;