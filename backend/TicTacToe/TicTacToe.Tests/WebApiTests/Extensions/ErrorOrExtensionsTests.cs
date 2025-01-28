using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using TicTacToe.Api.Extensions;

namespace TicTacToe.Tests.WebApiTests.Extensions;

public class ErrorOrExtensionsTests
{
    [Theory]
    [MemberData(nameof(ErrorsForToResult))]
    public void ToResult_ShouldReturnCorrectProblemDetailsForErrors(List<Error> errors, int expectedStatusCode)
    {
        // Arrange
        var errorOr = ErrorOr<string>.From(errors);

        // Act
        var response = errorOr.ToResult();

        // Assert
        var problemResult = Assert.IsType<ProblemHttpResult>(response);
        Assert.Equal(expectedStatusCode, problemResult.StatusCode);

        var problemDetails = problemResult.ProblemDetails;
        Assert.NotNull(problemDetails);
        Assert.Equal(errors[0].Description, problemDetails.Title);
    }

    public static IEnumerable<object[]> ErrorsForToResult()
    {
        yield return new object[]
        {
            new List<Error> { Error.Validation("ValidationError", "Validation failed") },
            StatusCodes.Status400BadRequest
        };

        yield return new object[]
        {
            new List<Error> { Error.NotFound("NotFoundError", "Resource not found") },
            StatusCodes.Status404NotFound
        };

        yield return new object[]
        {
            new List<Error> { Error.Failure("FailureError", "An unexpected error occurred") },
            StatusCodes.Status500InternalServerError
        };

        yield return new object[]
        {
            new List<Error> { Error.Unexpected("UnexpectedError", "Unexpected system failure") },
            StatusCodes.Status500InternalServerError
        };
    }
}