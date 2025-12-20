using System.Text.Json.Serialization;

namespace DmFors.WebApi.Shared.Results;

/// <summary>
/// Ошибка в приложении
/// </summary>
/// <param name="Code">Код ошибки в бизнес логике</param>
/// <param name="Message">Человеко читаемое сообщение об ошибке для клиента</param>
/// <param name="Type">Тип ошибки в бизнес логике</param>
/// <param name="Meta">Любая дополнительная информация (например, название поля, которое не прошло валидацию)</param>
public record Error(string Code, string Message, ErrorType Type, object? Meta = null)
{
    private const string SEPARATOR = "||";

    public Errors ToErrors() => new([this]);

    public static implicit operator Errors(Error error) => error.ToErrors();

    public string Serialize() => string.Join(SEPARATOR, Code, Message, Type);

    public static Error Deserialize(string serialized)
    {
        string[] parts = serialized.Split(SEPARATOR);

        if (parts.Length != 3)
            throw new FormatException("Invalid serialized format");

        if (Enum.TryParse(parts[2], out ErrorType type))
            throw new FormatException("Invalid serialized format");

        return new Error(parts[0], parts[1], type);
    }

    #region General

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);

    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);

    public static Error Validation(string invalidField, string code, string message) => new(
        code,
        message,
        ErrorType.Validation,
        new Dictionary<string, string> { ["invalidField"] = invalidField });

    public static Error Authentication(string code, string message) => new(code, message, ErrorType.Authentication);

    public static Error Authorization(string code, string message) => new(code, message, ErrorType.Authorization);

    #endregion
}

/// <summary>
/// Тип ошибки в бизнес логике
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    /// <summary>
    /// Сущность не найдена
    /// </summary>
    NotFound,

    /// <summary>
    /// Сущности конфликтуют
    /// </summary>
    Conflict,

    /// <summary>
    /// Ошибка сервера
    /// </summary>
    Failure,

    /// <summary>
    /// Параметр запроса не прошёл валидацию
    /// </summary>
    Validation,

    /// <summary>
    /// Ошибка аутентификации
    /// </summary>
    Authentication,

    /// <summary>
    /// Ошибка авторизации
    /// </summary>
    Authorization
}