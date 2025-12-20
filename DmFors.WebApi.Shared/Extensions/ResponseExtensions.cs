using DmFors.WebApi.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DmFors.WebApi.Shared.Extensions;

public static class ResponseExtensions
{
    public static ObjectResult ToResponse(this Error error) => CreateResponse(error, GetStatusCode(error));

    public static ObjectResult ToResponse(this IEnumerable<Error> errors) => CreateResponse(errors, GetStatusCode(errors));

    private static int GetStatusCode(IEnumerable<Error> errors)
    {
        var errorsArray = errors.ToArray();
        int uniqueErrorCount = errorsArray.DistinctBy(e => e.Type).Count();

        return uniqueErrorCount == 1 ? GetStatusCode(errorsArray.First()) : StatusCodes.Status500InternalServerError;
    }

    private static int GetStatusCode(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

    private static ObjectResult CreateResponse(object data, int statusCode)
    {
        return new ObjectResult(data) { StatusCode = statusCode };
    }
}