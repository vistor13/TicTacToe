using ErrorOr;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Application.Commands.ConsoleUI;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        renderer.RenderInstruction();
        return Result.Success;
    }
}