using System;

namespace GameRental.Core.Results;

public class ServiceResult
{
    public bool IsSuccess { get; private set; }
    public ErrorType ErrorType { get; private set; }

    public string Message { get; private set; } = string.Empty;


    private ServiceResult() { }

    public static ServiceResult Success()
    {
        return new ServiceResult { IsSuccess = true };
    }

    public static ServiceResult Fail(ErrorType errorType, string message)
    {
        return new ServiceResult { IsSuccess = false, ErrorType = errorType, Message = message };
    }
}

public class ServiceResult<T>
{
    public bool IsSuccess { get; private set; }
    public ErrorType ErrorType {get; private set;}
    public string Message { get; private set; } = string.Empty; 
    public T? Value { get; private set; }
    private ServiceResult()
    {
        
    }
    public static ServiceResult<T> Success(T value)
    {
        return new ServiceResult<T>{IsSuccess = true, Value = value};
    }
    public static ServiceResult<T> Fail(ErrorType errorType, string message)
    {
        return new ServiceResult<T> {IsSuccess = false, ErrorType = errorType, Message = message};
    }
}
