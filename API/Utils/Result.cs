namespace API.Utils;

public class Result<T>
{
    public Result()
    {
    }

    public Result(int statusCode)
    {
        StatusCode = statusCode;
    }

    public Result(int statusCode, T data)
    {
        StatusCode = statusCode;
        Data = data;
    }

    public Result(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}