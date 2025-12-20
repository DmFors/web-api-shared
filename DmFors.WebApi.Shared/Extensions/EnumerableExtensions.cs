using DmFors.WebApi.Shared.Results;

namespace DmFors.WebApi.Shared.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection) => collection == null || !collection.Any();
    
    public static Errors ToErrors(this IEnumerable<Error> errors) => new(errors.ToList());
}