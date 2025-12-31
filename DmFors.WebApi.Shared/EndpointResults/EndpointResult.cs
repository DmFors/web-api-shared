using CSharpFunctionalExtensions;
using DmFors.WebApi.Shared.Results;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace DmFors.WebApi.Shared.EndpointResults;

public class EndpointResult<TValue> : IResult
{
    private readonly IResult _result;
    
    public EndpointResult(Result<TValue, Errors> result)
    {
        _result = result.IsSuccess ? new SuccessResult<TValue>(result.Value) : new ErrorsResult(result.Error);
    }
    
    public EndpointResult(Result<TValue, Error> result)
    {
        _result = result.IsSuccess ? new SuccessResult<TValue>(result.Value) : new ErrorsResult(result.Error);
    }
    
    public EndpointResult(UnitResult<Errors> result)
    {
        _result = result.IsSuccess ? new SuccessResult<TValue>() : new ErrorsResult(result.Error);
    }
    
    public EndpointResult(TValue value)
    {
        _result = new SuccessResult<TValue>(value);
    }
    
    public EndpointResult(Errors errors)
    {
        _result = new ErrorsResult(errors);
    }

    public Task ExecuteAsync(HttpContext httpContext) => _result.ExecuteAsync(httpContext);
    
    public static implicit operator EndpointResult<TValue>(Result<TValue, Error> result) => new(result);
    
    public static implicit operator EndpointResult<TValue>(Result<TValue, Errors> result) => new(result);
    
    public static implicit operator EndpointResult<TValue>(UnitResult<Errors> result) => new(result);
    
    public static implicit operator EndpointResult<TValue>(TValue value) => new(value);
    
    public static implicit operator EndpointResult<TValue>(Errors errors) => new(errors);
}