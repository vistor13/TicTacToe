using ErrorOr;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Interfaces;

public interface ICommandInvoker
{
    ErrorOr<GameStateDto>? Execute(ICommand command, Dictionary<string, List<Type>> _commandsByState);
}