using GameRental.Core.Results;

namespace GameRental.Api.Helpers;

public static class ErrorResults
{
    public static IResult From(ErrorType errorType, string message)
    {
        return errorType switch
        {
            ErrorType.Validation => Results.BadRequest(new { error = message }),
            ErrorType.NotFound => Results.NotFound(new { error = message }),
            ErrorType.Conflict => Results.Conflict(new { error = message }),
            _ => Results.StatusCode(500)
        };
    }
}
