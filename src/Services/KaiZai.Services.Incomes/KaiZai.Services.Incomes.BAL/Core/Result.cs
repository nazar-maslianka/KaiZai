namespace KaiZai.Services.Incomes.BAL.Core;

/// <summary>
/// Represents the possible statuses of a process.
/// </summary>
public enum ProcessStatus
{
    /// <summary>
    /// The process completed successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// The process encountered a user-defined error.
    /// </summary>
    UserError,

    /// <summary>
    /// The process encountered a system or exception error.
    /// </summary>
    SystemError
}

/// <summary>
/// Represents a result container that can hold either a successful result value or an error message.
/// </summary>
/// <typeparam name="T">The type of the result value when successful.</typeparam>
public sealed record Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the result represents success.
    /// </summary>
    public ProcessStatus ProcessStatus { get; init; }

    /// <summary>
    /// Gets the result value when the operation is successful.
    /// </summary>
    public T Value { get; init; }

    /// <summary>
    /// Gets the user error message when the operation is not successful.
    /// </summary>
    public string? UserError { get; init; }

    /// <summary>
    /// Gets the system message when the operation is not successful.
    /// </summary>
    public string SystemError { get; init; }

    /// <summary>
    /// Creates a new successful result with the specified value.
    /// </summary>
    /// <param name="value">The successful result value.</param>
    /// <returns>A successful result instance.</returns>
    public static Result<T> Success(T value) => new Result<T> { ProcessStatus = ProcessStatus.Completed, Value = value };

    /// <summary>
    /// Creates a new failed result with the specified error message.
    /// </summary>
    /// <param name="userError">The user error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result<T> Failure(string userError) => new Result<T> { ProcessStatus = ProcessStatus.UserError, UserError = userError };

    /// <summary>
    /// Creates a new failed result with the specified error messages.
    /// </summary>
    /// <param name="userError">The user error message describing the failure.</param>
    /// <param name="systemError">The system error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result<T> Failure(string? userError, string systemError) => new Result<T> { ProcessStatus = ProcessStatus.SystemError, UserError = userError, SystemError = systemError };
}

/// <summary>
/// Represents a result container that can hold either a successful result or an error message.
/// </summary>
public sealed record Result
{
    /// <summary>
    /// Gets a value indicating whether the result represents success.
    /// </summary>
    public ProcessStatus ProcessStatus { get; init; }

    /// <summary>
    /// Gets the user error message when the operation is not successful.
    /// </summary>
    public string? UserError { get; init; }

    /// <summary>
    /// Gets the system error message when the operation is not successful.
    /// </summary>
    public string SystemError { get; init; }

    /// <summary>
    /// Creates a new successful result.
    /// </summary>
    /// <returns>A successful result instance.</returns>
    public static Result Success() => new Result { ProcessStatus = ProcessStatus.Completed };

    /// <summary>
    /// Creates a new failed result with the specified error message.
    /// </summary>
    /// <param name="userError">The user error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result Failure(string userError) => new Result { ProcessStatus = ProcessStatus.UserError, UserError = userError };

    /// <summary>
    /// Creates a new failed result with the specified error messages.
    /// </summary>
    /// <param name="userError">The user error message describing the failure.</param>
    /// <param name="systemError">The exception error message describing the failure.</param>
    /// <returns>A failed result instance.</returns>
    public static Result Failure(string? userError, string systemError) => new Result { ProcessStatus = ProcessStatus.SystemError, UserError = userError, SystemError = systemError };
}