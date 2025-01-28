using ErrorOr;

namespace TicTacToe.Api.Extensions;

/// <summary>
///     Provides extension methods for converting ErrorOr to HTTP results.
/// </summary>
public static class ErrorOrExtensions
{
    /// <summary>
    ///     Converts an <see cref="ErrorOr{T}" /> result to an ASP.NET Core <see cref="IResult" />.
    /// </summary>
    /// <typeparam name="T">The type of the successful result.</typeparam>
    /// <param name="result">The <see cref="ErrorOr{T}" /> instance to be converted.</param>
    /// <returns>
    ///     An <see cref="IResult" /> representing either a successful response (HTTP 200 OK)
    ///     or an error response (HTTP 400 Bad Request, 404 Not Found, or 500 Internal Server Error).
    /// </returns>
    public static IResult ToResult<T>(this ErrorOr<T> result)
    {
        return result.Match(
            Results.Ok,
            errors =>
            {
                var firstError = errors[0];

                var statusCode = firstError.Type switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                return Results.Problem(statusCode: statusCode, title: firstError.Description);
            }
        );
    }
}