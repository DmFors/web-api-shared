namespace DmFors.WebApi.Shared.Results;

public class ErrorException(Errors errors) : Exception(string.Join("; ", errors.Select(e => e.Message)))
{
    public ErrorException(Error error) : this(error.ToErrors())
    {
    }

    public Errors Errors => errors;
}