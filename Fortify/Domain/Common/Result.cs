namespace Fortify.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public string? ErrorCode { get; }

    protected Result(bool isSuccess, T? value, string? error, string? errorCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorCode = errorCode;
    }

    public static Result<T> Success(T value) => new(true, value, null, null);
    public static Result<T> Failure(string error, string? errorCode = null) => new(false, default, error, errorCode);
}

public class Result : Result<object>
{
    protected Result(bool isSuccess, string? error, string? errorCode)
        : base(isSuccess, null, error, errorCode) { }

    public static Result Success() => new(true, null, null);
    public static new Result Failure(string error, string? errorCode = null) => new(false, error, errorCode);
}