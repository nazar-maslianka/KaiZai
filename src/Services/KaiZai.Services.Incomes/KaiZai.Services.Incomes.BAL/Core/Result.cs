namespace KaiZai.Services.Incomes.BAL.Core;

/// <summary>
/// Represents a result container that can hold either a successful result value or an error message.
/// </summary>
/// <typeparam name="T">The type of the result value when successful.</typeparam>
public sealed record Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the result represents success.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the result value when the operation is successful.
    /// </summary>
    public T Value { get; init; }

    /// <summary>
    /// Gets the error message when the operation is not successful.
    /// </summary>
    public string Error { get; init; }

    /// <summary>
    /// Creates a new successful result with the specified value.
    /// </summary>
    /// <param name="value">The successful result value.</param>
    /// <returns>A successful result instance.</returns>
    public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };

    /// <summary>
    /// Creates a new failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
}

/// <summary>
/// Represents a result container that can hold either a successful result or an error message.
/// </summary>
public sealed record Result
{
    /// <summary>
    /// Gets a value indicating whether the result represents success.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the error message when the operation is not successful.
    /// </summary>
    public string Error { get; init; }

    /// <summary>
    /// Creates a new successful result.
    /// </summary>
    /// <returns>A successful result instance.</returns>
    public static Result Success() => new Result { IsSuccess = true };

    /// <summary>
    /// Creates a new failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result Failure(string error) => new Result { IsSuccess = false, Error = error };
}