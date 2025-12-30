using System.Collections;

namespace DmFors.WebApi.Shared.Results;

public class Errors(List<Error> errors) : IEnumerable<Error>
{
    private readonly List<Error> _errors = [..errors];

    public static implicit operator Errors(List<Error> errors) => new(errors);

    public static implicit operator Errors(Error error) => new([error]);

    public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region General

    public static Errors NotFound(string message, string? code = null) => Error.NotFound(message, code).ToErrors();

    public static Errors NotFound(Guid id) =>
        Error.NotFound($"record with id={id} not found", "record.not.found").ToErrors();

    public static Errors Conflict(string message, string? code = null) => Error.Conflict(message, code).ToErrors();

    public static Errors Failure(string message, string? code = null) => Error.Failure(message, code).ToErrors();

    public static Errors Validation(string invalidField, string message, string? code = null) =>
        Error.Validation(invalidField, message, code).ToErrors();

    public static Errors Authentication(string message, string? code = null) =>
        Error.Authentication(message, code).ToErrors();

    public static Errors Authorization(string message, string? code = null) =>
        Error.Authorization(message, code).ToErrors();

    #endregion
}