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

    public static Errors NotFound(string code, string message) => Error.NotFound(code, message).ToErrors();

    public static Errors NotFound(Guid id) =>
        Error.NotFound("record.not.found", $"record with id={id} not found").ToErrors();

    public static Errors Conflict(string code, string message) => Error.Conflict(code, message).ToErrors();

    public static Errors Failure(string code, string message) => Error.Failure(code, message).ToErrors();

    public static Errors Validation(string invalidField, string code, string message) =>
        Error.Validation(invalidField, code, message).ToErrors();

    public static Errors Authentication(string code, string message) => Error.Authentication(code, message).ToErrors();

    public static Errors Authorization(string code, string message) => Error.Authorization(code, message).ToErrors();

    #endregion
}