namespace TicTacToe.Core.Models;

public class OperationResult
{
    private OperationResult(bool isSuccess, string errorMessage = null!)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; private set; }

    public bool IsSuccess { get; private set; }

    public static OperationResult Success()
    {
        return new OperationResult(true);
    }

    public static OperationResult Failure(string errorMessage)
    {
        return new OperationResult(false, errorMessage);
    }
}