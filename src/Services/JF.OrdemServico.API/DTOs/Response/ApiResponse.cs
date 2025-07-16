using System.Net;

namespace JF.OrdemServico.API.DTOs.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ApiResponse<T> Ok(T data, string? message = null)
    {
        return new()
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.OK
        };
    }

    public static ApiResponse<T> Created(T data, string? message = null)
    {
        return new()
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.Created
        };
    }

    public static ApiResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {

        return new()
        {
            Success = false,
            Message = message,
            Data = default,
            StatusCode = statusCode
        };
    }
}