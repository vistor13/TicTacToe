using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.MoveCommand;

public record MoveCommand(Guid GameId, int Row, int Col) : IRequest<ErrorOr<Success>>;