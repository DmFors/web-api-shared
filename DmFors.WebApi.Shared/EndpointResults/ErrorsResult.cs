using DmFors.WebApi.Shared.Extensions;
using DmFors.WebApi.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace DmFors.WebApi.Shared.EndpointResults;

public class ErrorsResult(Errors errors) : IResult
{
    public ErrorsResult(Error error) : this(error.ToErrors())
    {
    }
    
    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var envelope = Envelope.Error(errors);
        
        httpContext.Response.StatusCode = GetStatusCode(errors);
        
        return httpContext.Response.WriteAsJsonAsync(envelope);
    }

    private int GetStatusCode(Errors errorsList)
    {
        if (errorsList.IsNullOrEmpty())
            return StatusCodes.Status500InternalServerError;

        var errorTypes = errorsList.Select(e => e.Type).Distinct().ToList();

        if (errorTypes.Count == 1)
            return GetStatusCode(errorTypes.First());
        
        return StatusCodes.Status500InternalServerError;
    }

    private int GetStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Authentication => StatusCodes.Status401Unauthorized,
            ErrorType.Authorization => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}