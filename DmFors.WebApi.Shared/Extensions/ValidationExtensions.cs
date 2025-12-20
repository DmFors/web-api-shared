using DmFors.WebApi.Shared.Results;
using FluentValidation.Results;

namespace DmFors.WebApi.Shared.Extensions;

public static class ValidationExtensions
{
    public static Errors ToErrors(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(e => Error.Validation(e.PropertyName, e.ErrorCode, e.ErrorMessage))
            .ToList();
    }
}