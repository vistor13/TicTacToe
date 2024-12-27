using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.BoardValidator
{
    public class BoundsValidator : IValidator
    {
        private const int LowerBound = 0;

        public OperationResult Validate(MoveParameters moveParameters, Board board)
        {
            if (moveParameters.Row >= LowerBound && moveParameters.Row < Board.BoardSize &&
                moveParameters.Col >= LowerBound && moveParameters.Col < Board.BoardSize)
                return OperationResult.Success();

            return OperationResult.Failure(Messages.Error.OutOfBoundsErrorMessage);
        }
    }
}