using System.Net;
using DmFors.WebApi.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace DmFors.WebApi.Shared.EndpointResults;

public class SuccessResult<TValue>(TValue? value = default) : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var envelope = Envelope.Ok(value);
        
        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        
        return httpContext.Response.WriteAsJsonAsync(envelope);
    }
}