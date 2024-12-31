using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class BoundsValidator : IValidator
    {
        private const int LowerBound = 0;

        public ErrorOr<Success> Validate(MoveParameters moveParameters, Board board)
        {
            return moveParameters.Row >= LowerBound && moveParameters.Row < Board.BoardSize &&
                   moveParameters.Col >= LowerBound && moveParameters.Col < Board.BoardSize
                ? Result.Success
                : Error.Validation(
                    "OutOfBounds",
                    Messages.Error.OutOfBoundsErrorMessage
                );
        }
    }
}