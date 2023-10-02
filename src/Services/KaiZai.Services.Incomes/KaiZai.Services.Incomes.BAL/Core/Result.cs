namespace KaiZai.Services.Incomes.BAL.Core;

/// <summary>
/// Represents a strongly typed object. 
/// Provides methods for return success, failure
/// </summary>
/// <param name="T">The type of Value in the result object.</param>
public sealed class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }
    public string ErrorForUser { get; set; }
    public string ExceptionError { get; set; }

    public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
    public static Result<T> Failure(string errorForUser) => new Result<T> { IsSuccess = false, ErrorForUser = errorForUser };
    public static Result<T> Failure(string errorForUser, string exceptionError) => new Result<T> { IsSuccess = false, ErrorForUser = errorForUser, ExceptionError = exceptionError };
}
/// <summary>
/// Represents a result object. 
/// Provides methods for return success, failure
/// </summary>
public sealed class Result
{
    public bool IsSuccess { get; set; }
    public string ErrorForUser { get; set; }
    public string ExceptionError { get; set; }
    public static Result Success() => new Result { IsSuccess = true };
    public static Result Failure(string errorForUser) => new Result { IsSuccess = false, ErrorForUser = errorForUser };
    public static Result Failure(string errorForUser, string exceptionError) => new Result { IsSuccess = false, ErrorForUser = errorForUser, ExceptionError = exceptionError};
}