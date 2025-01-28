using ErrorOr;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        renderer.RenderInstruction();
        return Result.Success;
    }
}