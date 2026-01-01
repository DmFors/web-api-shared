using System.Text.Json.Serialization;
using DmFors.WebApi.Shared.Extensions;

namespace DmFors.WebApi.Shared.Results;

public record Envelope<T>
{
    protected Envelope(T? value, Errors? errors)
    {
        Value = value;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public T? Value { get; }

    public Errors? Errors { get; }

    public bool IsSuccess => Errors.IsNullOrEmpty();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool IsFailure => !IsSuccess;

    public DateTime TimeGenerated { get; }

    public static Envelope<T> Ok(T? value = default) => new(value, null);

    public static Envelope<T> Error(Errors? errors = null) => new(default, errors);
}

public record Envelope : Envelope<object>
{
    private Envelope(object? value, Errors? errors) : base(value, errors)
    {
    }
    
    public static new Envelope Ok(object? value = default) => new(value, null);
    
    public static new Envelope Error(Errors? errors = null) => new(default, errors);

}