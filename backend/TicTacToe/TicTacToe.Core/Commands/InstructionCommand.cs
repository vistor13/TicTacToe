using ErrorOr;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        renderer.RenderInstruction();
        return Result.Success;
    }
}